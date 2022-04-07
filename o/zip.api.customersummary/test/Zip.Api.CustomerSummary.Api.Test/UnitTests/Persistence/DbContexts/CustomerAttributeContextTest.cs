using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Persistence.DbContexts;
using Zip.Api.CustomerSummary.Persistence.Interfaces;
using Attribute = Zip.Api.CustomerSummary.Domain.Entities.Consumers.Attribute;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Persistence.DbContexts
{
    public class CustomerAttributeContextTest
    {
        private readonly Mock<IDbContext> _dbContext;

        public CustomerAttributeContextTest()
        {
            _dbContext = new Mock<IDbContext>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNulLException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new CustomerAttributeContext(null);
            });
        }

        [Fact]
        public async Task Should_GetConsumerAttributes()
        {
            _dbContext.Setup(x => x.QueryAsync<Attribute>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new List<Attribute>
                {
                    new Attribute()
                });

            var ctx = new CustomerAttributeContext(_dbContext.Object);
            var result = await ctx.GetConsumerAttributesAsync(392);

            Assert.Single(result);
        }
    }
}
