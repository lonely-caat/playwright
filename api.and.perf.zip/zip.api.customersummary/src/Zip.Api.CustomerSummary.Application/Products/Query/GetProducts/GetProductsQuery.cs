using System.Collections.Generic;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Products;

namespace Zip.Api.CustomerSummary.Application.Products.Query.GetProducts
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
