using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Persistence.DbContexts;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Persistence.DbContexts
{
    public class ContactContextTest
    {
        private readonly Mock<IDbContext> _dbContext;

        public ContactContextTest()
        {
            _dbContext = new Mock<IDbContext>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNulLException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new ContactContext(null);
            });
        }

        [Fact]
        public async Task Should_GetContact()
        {
            _dbContext.Setup(x => x.QueryFirstOrDefaultAsync<ContactDto>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new ContactDto());

            var ctx = new ContactContext(_dbContext.Object);
            var c = await ctx.GetContactAsync(392);

            Assert.NotNull(c);
        }

        [Fact]
        public async Task Should_GetMobile()
        {
            var expected = "0492919223";
            _dbContext.Setup(x => x.ExecuteScalarAsync<string>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(expected);

            var ctx = new ContactContext(_dbContext.Object);
            var c = await ctx.GetMobileAsync(32);
            Assert.Equal(expected, c);
        }
    }
}
