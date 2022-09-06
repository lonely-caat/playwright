using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Products;
using Zip.Api.CustomerSummary.Persistence.DbContexts;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Persistence.DbContexts
{
    public class ProductContextTest
    {
        private readonly Mock<IDbContext> _dbContext;

        public ProductContextTest()
        {
            _dbContext = new Mock<IDbContext>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNulLException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new ProductContext(null);
            });
        }

        [Fact]
        public async Task Should_Get()
        {
            _dbContext.Setup(x => x.QueryAsync<Product>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new List<Product> {
                    new Product(),
                    new Product()
                });

            var ctx = new ProductContext(_dbContext.Object);
            var result = await ctx.GetAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task Should_GetById()
        {
            _dbContext.Setup(x => x.QueryFirstOrDefaultAsync<Product>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new Product());

            var ctx = new ProductContext(_dbContext.Object);
            var result = await ctx.GetAsync(3234);

            Assert.NotNull(result);
        }
    }
}
