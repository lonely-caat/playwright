using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Persistence.DbContexts;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Persistence.DbContexts
{
    public class CreditProfileContextTest
    {
        private readonly Mock<IDbContext> _dbContext;

        public CreditProfileContextTest()
        {
            _dbContext = new Mock<IDbContext>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNulLException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new CreditProfileContext(null);
            });
        }

        [Fact]
        public async Task Should_GetProfileAttributes()
        {
            _dbContext.Setup(x => x.QueryAsync<ProfileAttribute>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new List<ProfileAttribute>()
                {
                    new ProfileAttribute(),
                    new ProfileAttribute()
                });

            var ctx = new CreditProfileContext(_dbContext.Object);
            var pa = await ctx.GetProfileAttributesAsync(CreditProfileStateType.Active);

            Assert.Equal(2, pa.Count());
        }

        [Fact]
        public async Task Should_GetProfileClassifications()
        {
            _dbContext.Setup(x => x.QueryAsync<ProfileClassification>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new List<ProfileClassification>()
                {
                    new ProfileClassification(),
                    new ProfileClassification()
                });

            var ctx = new CreditProfileContext(_dbContext.Object);
            var pa = await ctx.GetProfileClassificationsAsync(CreditProfileStateType.Active);

            Assert.Equal(2, pa.Count());
        }

        [Fact]
        public async Task Should_GetStateTypeByConsumerId()
        {
            _dbContext.Setup(x => x.QueryFirstOrDefaultAsync<CreditProfileState>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new CreditProfileState());

            var ctx = new CreditProfileContext(_dbContext.Object);
            var result = await ctx.GetStateTypeByConsumerIdAsync(324);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_CreateCreditProfileState()
        {
            _dbContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>()));

            var ctx = new CreditProfileContext(_dbContext.Object);
            await ctx.CreateCreditProfileStateAsync(new CreditProfileState());

            _dbContext.Verify(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public async Task Should_CreateCreditProfileAttribute()
        {
            _dbContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>()));

            var ctx = new CreditProfileContext(_dbContext.Object);
            await ctx.CreateCreditProfileAttributeAsync(12, 23);

            _dbContext.Verify(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public async Task Should_CreateCreditProfileClassification()
        {
            _dbContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>()));

            var ctx = new CreditProfileContext(_dbContext.Object);
            await ctx.CreateCreditProfileClassificationAsync(12, 23);

            _dbContext.Verify(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>()), Times.Once);
        }

    }
}
