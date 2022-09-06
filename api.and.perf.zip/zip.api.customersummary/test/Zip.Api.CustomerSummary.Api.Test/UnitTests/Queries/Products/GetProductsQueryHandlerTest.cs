using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Products.Query.GetProducts;
using Zip.Api.CustomerSummary.Domain.Common.Constants;
using Zip.Api.CustomerSummary.Domain.Entities.Products;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Products
{
    public class GetProductsQueryHandlerTest
    {
        private readonly Mock<IProductContext> _productContext;
        private readonly Mock<IMemoryCache> _cache;

        public GetProductsQueryHandlerTest()
        {
            _productContext = new Mock<IProductContext>();
            _cache = new Mock<IMemoryCache>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new GetProductsQueryHandler(null, _cache.Object));
            Assert.Throws<ArgumentNullException>(() => new GetProductsQueryHandler(_productContext.Object, null));
        }

        public IMemoryCache GetMemoryCache()
        {
            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();

            var memoryCache = serviceProvider.GetService<IMemoryCache>();
            return memoryCache;
        }

        [Fact]
        public async Task Given_ItemCached_ShouldReturn_CachingItems()
        {
            var mc = GetMemoryCache();
            mc.Set(CacheKeys.Products, new List<Product>() {
                new Product()
                {
                    Id=123
                },
                new Product()
                {
                    Id=456
                },
                new Product()
                {
                    Id=382
                }
            });

            var handler = new GetProductsQueryHandler(_productContext.Object, mc);
            var result = await handler.Handle(new GetProductsQuery(), CancellationToken.None);

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task Given_NoItemsCached_ShouldRetrieve_FromDbContext()
        {
            _productContext.Setup(x => x.GetAsync())
                .ReturnsAsync(new List<Product>(){
                new Product(){
                    Id = 1
                }
                });

            var mc = GetMemoryCache();

            var handler = new GetProductsQueryHandler(_productContext.Object, mc);
            var result = await handler.Handle(new GetProductsQuery(), CancellationToken.None);

            result.Count().Should().Be(1);
        }
    }
}
