using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.V1
{
    public class GetConsumerQueryHandler : IRequestHandler<GetConsumerQuery, Consumer>
    {
        private const string DEFAULT_REFERRED_BY_MERCHANT_NAME = "Applied direct";
        
        private readonly IConsumerContext _consumerContext;
        
        private readonly IAddressContext _addressContext;
        
        private readonly IMerchantContext _merchantContext;
        
        public GetConsumerQueryHandler(
            IConsumerContext consumerContext,
            IAddressContext addressContext,
            IMerchantContext merchantContext)
        {
            _consumerContext = consumerContext ?? throw new ArgumentNullException(nameof(consumerContext));
            _addressContext = addressContext ?? throw new ArgumentNullException(nameof(addressContext));
            _merchantContext = merchantContext ?? throw new ArgumentNullException(nameof(merchantContext));
        }

        public async Task<Consumer> Handle(GetConsumerQuery request, CancellationToken cancellationToken)
        {
            var consumer = await _consumerContext.GetAsync(request.ConsumerId);

            if (consumer == null)
            {
                throw new ConsumerNotFoundException(request.ConsumerId);
            }

            if (consumer.OriginationMerchantId > 0)
            {
                consumer.ReferredBy = await _merchantContext.GetAsync(consumer.OriginationMerchantId.Value);
                consumer.IsExclusiveAccount = consumer.ReferredBy.Exclusive;
            }
            else if (consumer.ReferrerId != null)
            {
                consumer.ReferredBy = await _merchantContext.GetAsync(consumer.ReferrerId.Value);
            }
            else
            {
                consumer.ReferredBy ??= new Merchant { Name = DEFAULT_REFERRED_BY_MERCHANT_NAME };
            }

            consumer.RegistrationDate = Nullable.Compare(consumer.CreationDate, consumer.ActivationDate) >= 0
                ? consumer.CreationDate
                : consumer.ActivationDate;

            consumer.Documents = await _consumerContext.GetDocumentsAsync(request.ConsumerId);
            consumer.Address = await _addressContext.GetAsync(consumer.ConsumerId);

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
