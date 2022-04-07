using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.V1;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetDefaultPaymentMethod;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Payments;
using ZipMoney.Services.Payments.Contract.Payments;
using ZipMoney.Services.Payments.Contract.Types;

namespace Zip.Api.CustomerSummary.Application.Payments.Command.PayNow
{
    public class PayNowCommandHandler : IRequestHandler<PayNowCommand>
    {
        private readonly IMediator _mediator;
        private readonly IPaymentsServiceProxy _paymentService;

        public PayNowCommandHandler(IMediator mediator, IPaymentsServiceProxy paymentsService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _paymentService = paymentsService ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<Unit> Handle(PayNowCommand request, CancellationToken cancellationToken)
        {
            var consumer = await _mediator.Send(new GetConsumerQuery(request.ConsumerId), cancellationToken);

            if (consumer == null)
            {
                throw new ConsumerNotFoundException(request.ConsumerId);
            }

            if (!Enum.TryParse<CountryCode>(consumer.CountryId, out var cc))
            {
                throw new InvalidCountryCodeException(consumer.CountryId);
            }

            var account = await _mediator.Send(new GetAccountInfoQuery(request.ConsumerId), cancellationToken);

            if (account == null || account.AccountInfo == null)
            {
                throw new AccountNotFoundException(request.ConsumerId, default);
            }

            var paymentMethod = await _mediator.Send(new GetDefaultPaymentMethodQuery(consumer.ConsumerId), cancellationToken);

            if (paymentMethod == null)
            {
                throw new PaymentMethodNotFoundException(consumer.ConsumerId);
            }

            var makePaymentRequest = new MakePaymentRequest
            {
                AccountId = account.AccountInfo.AccountHash,
                Amount = request.Amount,
                CustomerId = $"{consumer.ConsumerId}",
                PayBy = PayBy.PaymentMethod,
                Reason = "Once-off payment",
                CountryCode = cc,
                PaymentMethodId = paymentMethod.Id,
                Type = PaymentType.OneOff,
                ReferenceNumber = GenerateReferenceNumber(paymentMethod),
                Channel = "CSP",
                Originator = new ZipMoney.Services.Payments.Contract.Common.PaymentOriginator
                {
                    CustomerName = $"{consumer.FirstName} {consumer.LastName}",
                    EmailAddress = request.OriginatorEmail,
                    IPAddress = request.OriginatorIpAddress
                }
            };

            var paymentResponse = await _paymentService.MakePayment(makePaymentRequest);

            if (!string.IsNullOrEmpty(paymentResponse.FailureReason))
            {
                throw new MakePaymentFailedException(paymentResponse.FailureReason);
            }

            return Unit.Value;
        }

        private static string GenerateReferenceNumber(PaymentMethodDto paymentMethodDto)
        {
            var pm = string.IsNullOrEmpty(paymentMethodDto.BSB) ? "DefaultCard" : "DefaultBank";
            
            return $"Acc#:{pm}:OneOff-Payment:{DateTime.Now:yyyymmddHHmmss}";
        }
    }
}
