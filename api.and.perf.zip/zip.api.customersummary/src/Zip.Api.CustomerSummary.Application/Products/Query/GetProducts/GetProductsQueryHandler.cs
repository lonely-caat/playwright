using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Zip.Api.CustomerSummary.Domain.Common.Constants;
using Zip.Api.CustomerSummary.Domain.Entities.Products;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Products.Query.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductContext _productContext;
        private readonly IMemoryCache _cache;

        public GetProductsQueryHandler(IProductContext productContext, IMemoryCache memoryCache)
        {
            _productContext = productContext ?? throw new ArgumentNullException(nameof(productContext));
            _cache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            if (!_cache.TryGetValue(CacheKeys.Products, out IEnumerable<Product> products))
            {
                products = await _productContext.GetAsync();
                if (products.IsNotEmpty())
                {
                    _cache.Set(CacheKeys.Products, products);
                }
            }

            return products;
        }
    }
}
