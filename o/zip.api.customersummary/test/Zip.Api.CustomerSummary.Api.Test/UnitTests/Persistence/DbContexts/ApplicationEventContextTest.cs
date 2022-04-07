using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Events;
using Zip.Api.CustomerSummary.Persistence.DbContexts;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Persistence.DbContexts
{
    public class ApplicationEventContextTest
    {
        private readonly Mock<IDbContext> _dbContext;

        public ApplicationEventContextTest()
        {
            _dbContext = new Mock<IDbContext>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNulLException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new ApplicationEventContext(null);
            });
        }

        [Fact]
        public async Task Given_Inserted_ShouldReturn_NewOne()
        {
            _dbContext.Setup(x => x.ExecuteScalarAsync<long>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(123);

            _dbContext.Setup(x => x.QueryFirstOrDefaultAsync<ApplicationEvent>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new ApplicationEvent());

            var ctx = new ApplicationEventContext(_dbContext.Object);
            var ae = await ctx.CreateAsync(new ApplicationEvent());

            Assert.NotNull(ae);
        }

        [Fact]
        public async Task Should_get()
        {
            _dbContext.Setup(x => x.QueryFirstOrDefaultAsync<ApplicationEvent>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new ApplicationEvent());

            var ctx = new ApplicationEventContext(_dbContext.Object);
            var ae = await ctx.GetAsync(332);

            Assert.NotNull(ae);
        }

        [Fact]
        public async Task Should_mark_as_unpublished()
        {
            _dbContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>()));


            var ctx = new ApplicationEventContext(_dbContext.Object);
            await ctx.MarkAsUnpublishedAsync(332);


            _dbContext.Verify(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>()), Times.Once);

        }
    }
}
