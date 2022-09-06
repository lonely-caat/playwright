using System.Collections.Generic;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;

namespace Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumerAttributes
{
    public class GetConsumerAttributesQuery : IRequest<IEnumerable<ConsumerAttributeDto>>
    {
        public long ConsumerId { get; set; }
    }
}
