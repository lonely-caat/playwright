using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Persistence.DbContexts;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Persistence.DbContexts
{
    public class ConsumerStatContextTest
    {
        private readonly Mock<IDbContext> _dbContext;

        public ConsumerStatContextTest()
        {
            _dbContext = new Mock<IDbContext>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ConsumerStatContext(null));
        }

        [Fact]
        public async Task Should_getbyid()
        {
            _dbContext.Setup(x => x.QuerySingleOrDefaultAsync<ConsumerStat>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new ConsumerStat());

            var ctx = new ConsumerStatContext(_dbContext.Object);
            var cs = await ctx.GetAsync(3928);

            Assert.NotNull(cs);
        }

        [Fact]
        public async Task Should_getbyconsumerid()
        {
            _dbContext.Setup(x => x.QueryFirstOrDefaultAsync<ConsumerStat>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new ConsumerStat());

            var ctx = new ConsumerStatContext(_dbContext.Object);
            var cs = await ctx.GetByConsumerIdAsync(201892);

            Assert.NotNull(cs);
        }
    }
}
