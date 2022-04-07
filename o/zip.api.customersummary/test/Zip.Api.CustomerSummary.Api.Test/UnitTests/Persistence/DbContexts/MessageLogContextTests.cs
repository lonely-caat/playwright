using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.MessageLog;
using Zip.Api.CustomerSummary.Persistence.DbContexts;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Persistence.DbContexts
{
    public class MessageLogContextTests
    {

        private readonly Mock<IDbContext> _dbContext;

        public MessageLogContextTests()
        {
            _dbContext = new Mock<IDbContext>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new MessageLogContext(null);
            });
        }

        [Fact]
        public async Task Should_Get()
        {
            _dbContext.Setup(x => x.QueryFirstOrDefaultAsync<MessageLog>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new MessageLog());

            var ctx = new MessageLogContext(_dbContext.Object);
            var result = await ctx.GetAsync(392);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Get_null()
        {
            _dbContext.Setup(x => x.QueryFirstOrDefaultAsync<MessageLog>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(null as MessageLog);

            var ctx = new MessageLogContext(_dbContext.Object);
            var result = await ctx.GetAsync(392);

            Assert.Null(result);
        }

        [Fact]
        public async Task Should_Insert()
        {
            _dbContext.Setup(x => x.QueryFirstOrDefaultAsync<MessageLog>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new MessageLog());

            _dbContext.Setup(x => x.ExecuteScalarAsync<long>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(30);

            var ctx = new MessageLogContext(_dbContext.Object);
            var result = await ctx.InsertAsync(392, Guid.NewGuid(), "test", "detail", new MessageLogSettings(), DateTime.Now, null);

            Assert.True(result);

            _dbContext.Verify(x => x.ExecuteScalarAsync<long>(It.IsAny<string>(), It.IsAny<object>()), Times.Exactly(2));
        }
    }
}
