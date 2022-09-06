using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumerAttributes
{
    public class GetConsumerAttributesQueryHandler : IRequestHandler<GetConsumerAttributesQuery, IEnumerable<ConsumerAttributeDto>>
    {
        private readonly IAttributeContext _attributeContext;

        public GetConsumerAttributesQueryHandler(IAttributeContext attributeContext)
        {
            _attributeContext = attributeContext ?? throw new ArgumentNullException(nameof(attributeContext));
        }

        public async Task<IEnumerable<ConsumerAttributeDto>> Handle(GetConsumerAttributesQuery request, CancellationToken cancellationToken)
        {
           return await _attributeContext.GetConsumerAttributesAsync(request.ConsumerId);
        }
    }
}
