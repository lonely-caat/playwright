using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Persistence.DbContexts;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Persistence.DbContexts
{
    public class AccountTypeContextTest
    {
        private readonly Mock<IDbContext> _dbContext;

        public AccountTypeContextTest()
        {
            _dbContext = new Mock<IDbContext>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNulLException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new AccountTypeContext(null);
            });
        }

        [Fact]
        public async Task Should_get_account_type()
        {
            _dbContext.Setup(x => x.QuerySingleOrDefaultAsync<AccountType>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new AccountType());

            var ctx = new AccountTypeContext(_dbContext.Object);
            var at = await ctx.GetAsync(392);

            Assert.NotNull(at);
        }
    }
}
