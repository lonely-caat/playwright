using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Persistence.DbContexts;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Persistence.DbContexts
{
    public class MerchantContextTest
    {
        private readonly Mock<IDbContext> _dbContext;

        public MerchantContextTest()
        {
            _dbContext = new Mock<IDbContext>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNulLException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new MerchantContext(null);
            });
        }

        [Fact]
        public async Task Should_Get()
        {
            _dbContext.Setup(x => x.QueryFirstOrDefaultAsync<Merchant>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new Merchant());

            var ctx = new MerchantContext(_dbContext.Object);
            var result = await ctx.GetAsync(392);
            
            Assert.NotNull(result);
        }
    }
}
