using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Domain.Entities.Countries;
using Zip.Api.CustomerSummary.Persistence.DbContexts;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Persistence.DbContexts
{
    public class ConsumerContextTest
    {
        private readonly Mock<IDbContext> _dbContext;

        private readonly Mock<ICountryContext> _countryContext;

        public ConsumerContextTest()
        {
            _dbContext = new Mock<IDbContext>();
            _countryContext = new Mock<ICountryContext>();
        }

        private ConsumerContext Ctx => new ConsumerContext(_dbContext.Object, _countryContext.Object);

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNulLException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new ConsumerContext(null, _countryContext.Object);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                new ConsumerContext(_dbContext.Object, null);
            });
        }

        [Fact]
        public async Task Given_ConsumerFound_ShouldGet_Country()
        {
            _dbContext.Setup(x => x.QueryFirstOrDefaultAsync<Consumer>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new Consumer());

            _countryContext.Setup(x => x.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new Country());

            var c = await Ctx.GetAsync(3928);

            Assert.NotNull(c.Country);
        }

        [Fact]
        public async Task Given_ConsumerNotFound_ShouldReturn_null()
        {
            _dbContext.Setup(x => x.QueryFirstOrDefaultAsync<Consumer>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(null as Consumer);

            _countryContext.Setup(x => x.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new Country());

            var c = await Ctx.GetAsync(3928);

            Assert.Null(c);
        }

        [Fact]
        public async Task Should_GetLinkedConsumerId()
        {
            long expected = 129;

            _dbContext.Setup(x => x.QueryFirstOrDefaultAsync<long?>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(expected);

            var c = await Ctx.GetLinkedConsumerIdAsync(382);
            Assert.Equal(expected, c);
        }

        [Fact]
        public async Task Should_UpdateDateOfBirth()
        {
            _dbContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>()));

            await Ctx.UpdateDateOfBirthAsync(392, new DateTime());

            _dbContext.Verify(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public async Task Should_UpdateGender()
        {
            _dbContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>()));

            await Ctx.UpdateGenderAsync(392, Gender.FemaleWithKids);

            _dbContext.Verify(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public async Task Should_UpdateName()
        {
            _dbContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>()));

            _dbContext.Setup(x => x.QueryFirstOrDefaultAsync(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(null);

            await Ctx.UpdateNameAsync(392, "Shan","Ke","Mic","Mouse");

            _dbContext.Verify(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>()), Times.Exactly(2));
            _dbContext.Verify(x => x.QueryFirstOrDefaultAsync(It.IsAny<string>(), It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public async Task Should_GetDocuments()
        {
            _dbContext.Setup(x => x.QueryAsync<Document>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new List<Document>()
                {
                    new Document(),
                    new Document()
                });

            var r = await Ctx.GetDocumentsAsync(3029);

            Assert.Equal(2, r.Count());
        }

        [Fact]
        public async Task Should_GetAccountInfo()
        {
            _dbContext.Setup(x => x.QueryFirstOrDefaultAsync<AccountInfo>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new AccountInfo());

            var r = await Ctx.GetAccountInfoAsync(382);

            Assert.NotNull(r);
        }

        [Fact]
        public async Task Should_SetTrustScore()
        {
            await Ctx.SetTrustScoreAsync(It.IsAny<long>(), It.IsAny<int>());

            _dbContext.Verify(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public async Task Should_GetConsumer_By_CustomerIdAndProduct()
        {
            _dbContext.Setup(x => x.QueryFirstOrDefaultAsync<Consumer>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new Consumer());

            var r = await Ctx.GetByCustomerIdAndProductAsync(Guid.NewGuid(), 0);

            Assert.NotNull(r);
        }
    }
}
