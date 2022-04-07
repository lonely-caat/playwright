using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Countries.Query.GetCountries;
using Zip.Api.CustomerSummary.Domain.Common.Constants;
using Zip.Api.CustomerSummary.Domain.Entities.Countries;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Countries
{
    public class GetCountriesQueryHandlerTest
    {
        private readonly Mock<ICountryContext> _countryContext;
        private readonly Mock<IMemoryCache> _memoryCache;

        public GetCountriesQueryHandlerTest()
        {
            _countryContext = new Mock<ICountryContext>();
            _memoryCache = new Mock<IMemoryCache>();
        }

        public IMemoryCache GetMemoryCache()
        {
            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();

            var memoryCache = serviceProvider.GetService<IMemoryCache>();
            return memoryCache;


        }

        [Fact]
        public void Given_NullInject_Should_throw()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetCountriesQueryHandler(null, _memoryCache.Object);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetCountriesQueryHandler(_countryContext.Object, null);
            });
        }

        [Fact]
        public async Task Given_CachingItems_Should_return_cachingItems()
        {
            var mc = GetMemoryCache();

            var cachingCountries = new List<Country>
            {
                new Country()
                {
                    Id = "AU",
                    Name = "Australia"
                },
                new Country()
                {
                    Id = "NZ",
                    Name = "New Zealand"
                }
            };

            mc.Set(CacheKeys.Products, cachingCountries);

            var handler = new GetCountriesQueryHandler(_countryContext.Object, mc);
            var result = await handler.Handle(new GetCountriesQuery(), CancellationToken.None);

            Assert.Equal(cachingCountries, result);
        }

        [Fact]
        public async Task Given_CachingItemsNotFound_Should_call_context()
        {
            var expected = new List<Country>
            {
                new Country()
                {
                    Id = "AU",
                    Name = "Australia"
                },
                new Country()
                {
                    Id = "NZ",
                    Name = "New Zealand"
                }
            };
            _countryContext.Setup(x => x.GetCountriesAsync())
                .ReturnsAsync(expected);

            var mc = GetMemoryCache();
            var handler = new GetCountriesQueryHandler(_countryContext.Object, mc);
            var result = await handler.Handle(new GetCountriesQuery(), CancellationToken.None);

            Assert.Equal(expected, result);
        }
    }
}
