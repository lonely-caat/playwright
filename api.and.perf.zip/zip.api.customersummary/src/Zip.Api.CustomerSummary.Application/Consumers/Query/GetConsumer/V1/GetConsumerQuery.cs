using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;

namespace Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.V1
{
    public class GetConsumerQuery : IRequest<Consumer>
    {
        public long ConsumerId { get; set; }

        public GetConsumerQuery(long consumerId)
        {
            ConsumerId = consumerId;
        }

        public GetConsumerQuery()
        {
        }
    }
}
