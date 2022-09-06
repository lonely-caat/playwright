using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Infrastructure.Services.CustomerProfileService.Interfaces;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.V2
{
    public class GetConsumerQueryV2Handler : IRequestHandler<GetConsumerQueryV2, Consumer>
    {
        private const string DEFAULT_REFERRED_BY_MERCHANT_NAME = "Applied direct";

        private readonly IConsumerContext _consumerContext;

        private readonly IMerchantContext _merchantContext;

        private readonly ICustomerProfileService _customerProfileService;

        public GetConsumerQueryV2Handler(
            IConsumerContext consumerContext,
            IMerchantContext merchantContext,
            ICustomerProfileService customerProfileGraphQlService)
        {
            _consumerContext = consumerContext ?? throw new ArgumentNullException(nameof(consumerContext));
            _merchantContext = merchantContext ?? throw new ArgumentNullException(nameof(merchantContext));
            _customerProfileService = customerProfileGraphQlService ?? throw new ArgumentNullException(nameof(customerProfileGraphQlService));
        }

        public async Task<Consumer> Handle(GetConsumerQueryV2 request, CancellationToken cancellationToken)
        {
            var consumer = await _consumerContext.GetAsyncV2(request.ConsumerId);

            if (consumer == null)
            {
                return null;
            }

            var consumerInfo = await _customerProfileService.GetConsumerPersonalInfo(consumer.CustomerId.ToString());

            if (consumerInfo == null)
            {
                return null;
            }

            consumer.FirstName = consumerInfo.FirstName;
            consumer.LastName = consumerInfo.LastName;
            consumer.Gender = consumerInfo.Gender;
            consumer.Address = consumerInfo.Address;
            consumer.DateOfBirth = Convert.ToDateTime(consumerInfo.DateOfBirth);

            var referredByMerchantId = consumer.OriginationMerchantId ?? consumer.ReferrerId;

            if (referredByMerchantId > 0)
            {
                consumer.ReferredBy = await _merchantContext.GetAsync(referredByMerchantId.Value);
            }

            consumer.ReferredBy ??= new Merchant { Name = DEFAULT_REFERRED_BY_MERCHANT_NAME };

            consumer.IsExclusiveAccount = consumer.ReferredBy.Exclusive;

            consumer.RegistrationDate = Nullable.Compare(consumer.CreationDate, consumer.ActivationDate) >= 0
                                            ? consumer.CreationDate
                                            : consumer.ActivationDate;

            consumer.Documents = await _consumerContext.GetDocumentsAsync(request.ConsumerId);

            var linkedConsumerId = await _consumerContext.GetLinkedConsumerIdAsync(consumer.ConsumerId);

            if (linkedConsumerId > 0)
            {
                consumer.LinkedConsumer = await _consumerContext.GetAsync(linkedConsumerId.Value);
                consumer.LinkedAccount = await _consumerContext.GetAccountInfoAsync(linkedConsumerId.Value);
            }

            return consumer;
        }
    }
}