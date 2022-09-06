using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;

namespace Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.V2
{
    public class GetConsumerQueryV2  : IRequest<Consumer>
    {
        [FromRoute(Name = "consumerId")]
        public long ConsumerId { get; set; }

        public GetConsumerQueryV2(long consumerId)
        {
            ConsumerId = consumerId;
        }

        public GetConsumerQueryV2()
        {
        }
    }
}