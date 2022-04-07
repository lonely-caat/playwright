using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo;
using Zip.Api.CustomerSummary.Application.Common.Exceptions;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.V1;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetContact;
using Zip.Api.CustomerSummary.Domain.Entities.MessageLog;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;
using Zip.Api.CustomerSummary.Infrastructure.Configuration.OutgoingMessages;
using Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.PayNowUrlGenerator.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.PayNowUrlGenerator.Models;
using Zip.Api.CustomerSummary.Infrastructure.Services.ZipUrlShorteningService.Interfaces;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Communications.Command.SendPayNowLink
{
    public class SendPayNowLinkCommandHandler : IRequestHandler<SendPayNowLinkCommand>
    {
        private const string SmsContentName = "PayNow Link";
        private readonly IPayNowUrlGenerator _payNowUrlGenerator;
        private readonly IZipUrlShorteningService _zipUrlShorteningService;
        private readonly IMediator _mediator;
        private readonly IPayNowAccountContext _payNowAccountContext;
        private readonly IMessageLogContext _messageLogContext;
        private readonly ICommunicationsServiceProxy _communicationsServiceProxy;
        private readonly IOutgoingMessagesConfig _outgoingMessageConfig;

        public SendPayNowLinkCommandHandler(
            IPayNowUrlGenerator payNowUrlGenerator,
            IZipUrlShorteningService zipUrlShorteningService,
            IMediator mediator,
            IPayNowAccountContext payNowAccountContext,
            IMessageLogContext messageLogContext,
            ICommunicationsServiceProxy communicationsServiceProxy,
            IOutgoingMessagesConfig outgoingMessageConfig)
        {
            _payNowUrlGenerator = payNowUrlGenerator ?? throw new ArgumentNullException(nameof(payNowUrlGenerator));
            _zipUrlShorteningService = zipUrlShorteningService ?? throw new ArgumentNullException(nameof(zipUrlShorteningService));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _payNowAccountContext = payNowAccountContext ?? throw new ArgumentNullException(nameof(payNowAccountContext));
            _messageLogContext = messageLogContext ?? throw new ArgumentNullException(nameof(messageLogContext));
            _communicationsServiceProxy = communicationsServiceProxy ?? throw new ArgumentNullException(nameof(communicationsServiceProxy));
            _outgoingMessageConfig = outgoingMessageConfig ?? throw new ArgumentNullException(nameof(outgoingMessageConfig));
        }

        public async Task<Unit> Handle(SendPayNowLinkCommand request, CancellationToken cancellationToken)
        {
            var consumer = await _mediator.Send(new GetConsumerQuery(request.ConsumerId), cancellationToken);

            if (consumer == null)
            {
                throw new ConsumerNotFoundException(request.ConsumerId);
            }

            var account = await _mediator.Send(new GetAccountInfoQuery(request.ConsumerId), cancellationToken);

            if (account == null)
            {
                throw new AccountNotFoundException(request.ConsumerId, default);
            }

            var contact = await _mediator.Send(new GetContactQuery(request.ConsumerId), cancellationToken);

            if (contact == null)
            {
                throw new PaymentMethodNotFoundException(request.ConsumerId);
            }

            var paynowlinkAccount = await _payNowAccountContext.GetPayNowLinkAccountAsync(account.AccountInfo.AccountId);

            if (paynowlinkAccount == null)
            {
                throw new AccountTypeNotFoundException(account.AccountInfo.AccountId);
            }

            dynamic payNow;
            dynamic shortPayNowUrl;
            
            if(_outgoingMessageConfig.NewPayNowGenerator)
            {
                var generatePayNowUrlRequest = new GeneratePayNowUrlRequest
                {
                    ConsumerId = request.ConsumerId,
                    Amount = request.Amount
                };
                
                payNow = await _payNowUrlGenerator.GeneratePayNowUrlAsync(generatePayNowUrlRequest);
                
                if (payNow?.PayNowUrl == null)
                {
                    throw new PayNowUrlGenerationFailedException($"Failed to call GeneratePayNowUrlAsync for ConsumerId:{request.ConsumerId}");
                }
                
                shortPayNowUrl = payNow.PayNowUrl;
            }
            else
            {
                payNow = await _payNowUrlGenerator.GeneratePayNowUrlAsync(paynowlinkAccount.Classification, paynowlinkAccount.CountryId, request.Amount, $"{account.AccountInfo.AccountId}");

                if (payNow == null)
                {
                    throw new PayNowUrlGenerationFailedException(account.AccountInfo.AccountId, request.Amount, paynowlinkAccount.CountryId.ToString(), paynowlinkAccount.Classification.DisplayName());
                }

                shortPayNowUrl = await _zipUrlShorteningService.GetZipShortenedUrlAsync(payNow.PayNowUrl);

                if (string.IsNullOrEmpty(shortPayNowUrl))
                {
                    throw new ShorteningUrlFailedException(payNow.PayNowUrl);
                }
            }

            var smsContent = await _communicationsServiceProxy.GetSmsContentAsync(SmsContentName);

            if (smsContent == null)
            {
                throw new SmsContentNotFoundException(SmsContentName);
            }

            await _communicationsServiceProxy.SendPayNowLinkAsync(new Infrastructure.Services.CommunicationsService.Models.SendPayNowLink
            {
                Classification = paynowlinkAccount.Classification.DisplayName(),
                FirstName = consumer.FirstName,
                Message = smsContent.Content,
                PhoneNumber = contact.Mobile,
                PayNowUrl = shortPayNowUrl,
                ConsumerId = request.ConsumerId,
                AccountId = account.AccountInfo.AccountId
            });

            return Unit.Value;
        }

        private async Task SendInsertCommentAsync(SendPayNowLinkCommand request, string firstName, string shortPayNowUrl)
        {
            await _messageLogContext.InsertAsync(
                request.ConsumerId,
                Guid.NewGuid(),
                $"Hi {firstName}, click here to make a payment on your Zip Pay {shortPayNowUrl}",
                string.Empty,
                new MessageLogSettings
                {
                    DeliveryMethod = MessageLogDeliveryMethod.SMS,
                    Category = MessageLogCategory.Consumer,
                    Type = MessageLogType.PaymentArrearsConfiguredSms,
                    Status = MessageLogStatus.Sent
                },
                DateTime.Now);
        }
    }
}