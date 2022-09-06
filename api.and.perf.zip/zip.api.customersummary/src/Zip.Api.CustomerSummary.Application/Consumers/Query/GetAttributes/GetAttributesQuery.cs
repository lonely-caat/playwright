using System.Collections.Generic;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;

namespace Zip.Api.CustomerSummary.Application.Consumers.Query.GetAttributes
{
    public class GetAttributesQuery : IRequest<IEnumerable<Attribute>>
    {
    }
}
