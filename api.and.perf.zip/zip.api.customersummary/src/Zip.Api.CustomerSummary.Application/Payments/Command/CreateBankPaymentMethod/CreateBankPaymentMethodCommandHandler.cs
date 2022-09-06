using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.V1;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Payments;
using ZipMoney.Services.Payments.Contract.Common;
using ZipMoney.Services.Payments.Contract.PaymentMethods;
using ZipMoney.Services.Payments.Contract.Types;

namespace Zip.Api.CustomerSummary.Application.Payments.Command.CreateBankPaymentMethod
{
    public class CreateBankPaymentMethodCommandHandler : IRequestHandler<CreateBankPaymentMethodCommand, PaymentMethodResponse>
    {
        private readonly IPaymentsServiceProxy _paymentsServiceProxy;

        private readonly IMediator _mediator;

        public CreateBankPaymentMethodCommandHandler(IPaymentsServiceProxy proxy, IMediator mediator)
        {
            _paymentsServiceProxy = proxy ?? throw new ArgumentNullException(nameof(proxy));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<PaymentMethodResponse> Handle(CreateBankPaymentMethodCommand request, CancellationToken cancellationToken)
        {
            var consumer = await _mediator.Send(new GetConsumerQuery(request.ConsumerId), cancellationToken);

            if (consumer == null || consumer.ConsumerId <= 0)
            {
                throw new ConsumerNotFoundException(request.ConsumerId);
            }

            if (!Enum.TryParse<CountryCode>(consumer.CountryId, out var countryCode))
            {
                throw new InvalidCountryCodeException(consumer.CountryId);
            }

            var account = await _mediator.Send(new GetAccountInfoQuery(request.ConsumerId), cancellationToken);

            if (account?.AccountInfo == null)
            {
                throw new AccountNotFoundException(request.ConsumerId, default);
            }

            if (!Enum.TryParse<ProductClassification>(account.AccountInfo.Product.ToString(), out var product))
            {
                throw new InvalidProductCodeException(account.AccountInfo.Product.ToString());
            }

            var createPaymentMethodRequest = new CreatePaymentMethodRequest
            {
                Id = Guid.NewGuid(),
                CustomerId = request.ConsumerId.ToString(),
                CountryCode = countryCode,
                Name = "Bank",
                Product = product,
                Originator = new PaymentOriginator { EmailAddress = request.OriginatorEmail },
                Bank = new BankPaymentMethodRequest
                {
                    AccountName = request.AccountName,
                    AccountNumber = request.AccountNumber,
                    BSB = request.BSB
                }
            };

            var response = await _paymentsServiceProxy.CreatePaymentMethod(createPaymentMethodRequest);

            if (response == null)
            {
                throw new CreatePaymentFailedException($"Payment proxy returned a null response.");
            }
            
            if (request.IsDefault)
            {
                await _paymentsServiceProxy.SetDefaultPaymentMethod(response.Id, createPaymentMethodRequest.CustomerId);
            }

            return response;
        }
    }
}
