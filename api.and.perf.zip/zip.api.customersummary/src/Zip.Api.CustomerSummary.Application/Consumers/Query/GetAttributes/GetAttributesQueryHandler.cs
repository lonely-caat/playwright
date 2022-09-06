using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Attribute = Zip.Api.CustomerSummary.Domain.Entities.Consumers.Attribute;

namespace Zip.Api.CustomerSummary.Application.Consumers.Query.GetAttributes
{
    public class GetAttributesQueryHandler : IRequestHandler<GetAttributesQuery, IEnumerable<Attribute>>
    {
        private readonly IAttributeContext _attributeContext;

        public GetAttributesQueryHandler(IAttributeContext attributeContext)
        {
            _attributeContext = attributeContext ?? throw new ArgumentNullException(nameof(attributeContext));
        }

        public async Task<IEnumerable<Attribute>> Handle(GetAttributesQuery request, CancellationToken cancellationToken)
        {
           return await _attributeContext.GetAttributesAsync();
        }
    }
}
