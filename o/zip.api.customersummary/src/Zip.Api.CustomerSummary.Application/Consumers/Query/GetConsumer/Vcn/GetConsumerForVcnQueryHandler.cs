using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Domain.Entities.Products;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.Vcn
{
    public class GetConsumerForVcnQueryHandler : IRequestHandler<GetConsumerForVcnQuery, Consumer>
    {
        private readonly IConsumerContext _consumerContext;

        public GetConsumerForVcnQueryHandler(IConsumerContext consumerContext)
        {
            _consumerContext = consumerContext ?? throw new ArgumentNullException(nameof(consumerContext));
        }

        public async Task<Consumer> Handle(GetConsumerForVcnQuery request, CancellationToken cancellationToken)
        {
            var product = (int)Enum.Parse(typeof(ProductClassification), request.Product);
            var consumer = await _consumerContext.GetByCustomerIdAndProductAsync(request.CustomerId, product);

            if (consumer == null)
            {
                throw new ConsumerNotFoundException(request.CustomerId.ToString());
            }

            return consumer;
        }
    }
}
