using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Transactions;
using Zip.Api.CustomerSummary.Persistence.DbContexts;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Persistence.DbContexts
{
    public class TransactionContextTest
    {
        private readonly Mock<IDbContext> _dbContext;

        public TransactionContextTest()
        {
            _dbContext = new Mock<IDbContext>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNulLException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new TransactionHistoryContext(null);
            });
        }

        [Fact]
        public async Task Should_Find()
        {
            _dbContext.Setup(x => x.QueryAsync<TransactionHistory>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new List<TransactionHistory>
                {
                    new TransactionHistory()
                });

            var ctx = new TransactionHistoryContext(_dbContext.Object);
            var result = await ctx.FindByConsumerIdAsync(3921, DateTime.Now.AddMonths(-3), DateTime.Now);

            Assert.Single(result);
        }
    }
}
