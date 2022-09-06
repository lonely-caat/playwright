using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Prometheus;
using System;
using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.V1;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetContact;
using Zip.Api.CustomerSummary.Domain.Common.Constants;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.Kinesis;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.KinesisProducer.Interfaces;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Core.Prometheus;
using Zip.Services.Accounts.Contract.Account.Status;

namespace Zip.Api.CustomerSummary.Application.Accounts.Command.CloseAccount
{
    public class CloseAccountCommandHandler : IRequestHandler<CloseAccountCommand>
    {
        private readonly ICreditProfileContext _creditProfileContext;
        private readonly IKinesisProducer _kinesisProducer;
        private readonly KinesisSettings _settings;
        private readonly IAccountsService _accountsService;
        private readonly IMediator _mediator;
        private readonly ICommunicationsServiceProxy _communicationsServiceProxy;
        private readonly Counter _failedCloseAccountCount;
        private readonly Counter _closeAccountCount;

        public CloseAccountCommandHandler(
            ICreditProfileContext creditProfileContext,
            IKinesisProducer kinesisProducer,
            IOptions<KinesisSettings> options,
            IAccountsService accountsService,
            IMediator mediator,
            ICommunicationsServiceProxy communicationsServiceProxy)
        {
            _creditProfileContext = creditProfileContext ?? throw new ArgumentNullException(nameof(creditProfileContext));
            _kinesisProducer = kinesisProducer ?? throw new ArgumentNullException(nameof(kinesisProducer));
            _settings = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _accountsService = accountsService;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _communicationsServiceProxy = communicationsServiceProxy;
            _failedCloseAccountCount = MetricsHelper.GetCounter(PrometheusKeys.FailedCloseAccount, "Failed Close Account Count");
            _closeAccountCount = MetricsHelper.GetCounter(PrometheusKeys.CloseAccount, "Close Account Count");
        }
        
        public async Task<Unit> Handle(CloseAccountCommand request, CancellationToken cancellationToken)
        {
            var consumer = await _mediator.Send(new GetConsumerQuery(request.ConsumerId), cancellationToken);
            
            if (consumer == null)
            {
                throw new ConsumerNotFoundException(request.ConsumerId);
            }

            var accountInfo = await _mediator.Send(new GetAccountInfoQuery(request.ConsumerId), cancellationToken);

            if (accountInfo == null)
            {
                throw new AccountNotFoundException(request.ConsumerId, default);
            }

            try
            {
                await _accountsService.CloseAccount(request.AccountId, new CloseAccountRequest { ChangedBy = request.ChangedBy }, Guid.NewGuid().ToString());
            }
            catch (Refit.ApiException apiEx) when (apiEx.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
            {
                var msg = await apiEx.GetContentAsAsync<ExMsg>();
                _failedCloseAccountCount.Inc();
                throw new CloseAccountUnprocessableException(msg.Message, apiEx);
            }

            var currentState = new CreditProfileState()
            {
                ChangedBy = request.ChangedBy,
                Comments = request.Comments,
                CreditProfileId = request.CreditProfileId,
                CreditStateType = request.CreditStateType.Value
            };

            await _creditProfileContext.CreateCreditProfileStateAsync(currentState);

            if (request.ProfileClassificationId.HasValue)
            {
                await _creditProfileContext.CreateCreditProfileClassificationAsync(request.CreditProfileId, request.ProfileClassificationId.Value);
            }

            if (request.ProfileAttributeId.HasValue)
            {
                await _creditProfileContext.CreateCreditProfileAttributeAsync(request.CreditProfileId, request.ProfileAttributeId.Value);
            }

            var kinesisRecord = new KinesisCustomerRecord()
            {
                ConsumerId = request.ConsumerId,
                CreditProfileStateType = currentState.CreditStateType.ToString()
            };

            await _kinesisProducer.PutRecord(_settings.KinesisStreamName, JsonConvert.SerializeObject(kinesisRecord, SerializerSettings), $"{request.ConsumerId}");

            var contact = await _mediator.Send(new GetContactQuery(request.ConsumerId), cancellationToken);

            if (contact != null && !string.IsNullOrEmpty(contact.Email))
            {
                await _communicationsServiceProxy.SendCloseAccountEmailAsync(
                    new Infrastructure.Services.CommunicationsService.Models.CloseAccount
                    {
                        AccountNumber = request.AccountId.ToString(),
                        Email = contact.Email,
                        FirstName = consumer.FirstName,
                        Product = accountInfo.AccountInfo.Product.Value.DisplayName()
                    });
            }

            _closeAccountCount.Inc();

            return Unit.Value;
        }
        private readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public class ExMsg
        {
            public string Message { get; set; }
        }
    }
}
