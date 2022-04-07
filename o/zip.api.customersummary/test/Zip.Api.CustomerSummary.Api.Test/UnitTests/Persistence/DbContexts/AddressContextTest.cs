using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Domain.Entities.Countries;
using Zip.Api.CustomerSummary.Persistence.DbContexts;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Persistence.DbContexts
{
    public class AddressContextTest
    {
        private readonly Mock<IDbContext> _dbContext;

        private readonly Mock<ICountryContext> _countryContext;

        public AddressContextTest()
        {
            _dbContext = new Mock<IDbContext>();
            _countryContext = new Mock<ICountryContext>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNulLException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new AddressContext(null, _countryContext.Object);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                new AddressContext(_dbContext.Object, null);
            });
        }

        [Fact]
        public async Task Given_AddressFound_ShouldHave_Country()
        {
            _dbContext.Setup(x => x.QueryFirstOrDefaultAsync<Address>(It.IsAny<string>(), It.IsAny<object>()))
                        .ReturnsAsync(new Address() { Id = 372, CountryCode = "NZ" });

            _countryContext.Setup(x => x.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new Country());

            var ctx = new AddressContext(_dbContext.Object, _countryContext.Object);
            var addr = await ctx.GetAsync(3827);

            Assert.NotNull(addr.Country);
        }

        [Fact]
        public async Task Given_AddressFound_ButWithout_CountryCode_ShouldNotHave_Country()
        {
            _dbContext.Setup(x => x.QueryFirstOrDefaultAsync<Address>(It.IsAny<string>(), It.IsAny<object>()))
                        .ReturnsAsync(new Address() { Id = 372 });

            _countryContext.Setup(x => x.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new Country());

            var ctx = new AddressContext(_dbContext.Object, _countryContext.Object);
            var addr = await ctx.GetAsync(3827);

            Assert.NotNull(addr);
            Assert.Null(addr.Country);
        }

        [Fact]
        public async Task Should_update()
        {
            _dbContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>()));
            _dbContext.Setup(x => x.ExecuteScalarAsync<long>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(932);

            var ctx = new AddressContext(_dbContext.Object, _countryContext.Object);
            await ctx.UpdateAsync(3827, new Address());

            _dbContext.Verify(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>()), Times.Exactly(2));
            _dbContext.Verify(x => x.ExecuteScalarAsync<long>(It.IsAny<string>(), It.IsAny<object>()), Times.Once);
        }
    }
}
