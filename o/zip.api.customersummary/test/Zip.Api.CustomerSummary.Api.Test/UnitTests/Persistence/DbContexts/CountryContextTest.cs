using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Countries;
using Zip.Api.CustomerSummary.Persistence.DbContexts;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Persistence.DbContexts
{
    public class CountryContextTest
    {
        private readonly Mock<IDbContext> _dbContext;

        public CountryContextTest()
        {
            _dbContext = new Mock<IDbContext>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNulLException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new CountryContext(null);
            });
        }

        [Fact]
        public async Task Should_Get()
        {
            _dbContext.Setup(x => x.QueryFirstOrDefaultAsync<Country>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new Country());

            var ctx = new CountryContext(_dbContext.Object);
            var c = await ctx.GetAsync("AU");

            Assert.NotNull(c);
        }

        [Fact]
        public async Task Should_GetCountries()
        {
            _dbContext.Setup(x => x.QueryAsync<Country>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new List<Country>() { 
                    new Country()
                    {
                        Id = "AU",
                        Name = "Australia"
                    },
                    new Country()
                    {
                        Id="NZ",
                        Name = "New Zealand"
                    }
                });

            var ctx = new CountryContext(_dbContext.Object);
            var c = await ctx.GetCountriesAsync();

            Assert.NotNull(c);
            Assert.Equal(2, c.Count());
        }
    }
}
