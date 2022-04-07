using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Zip.Api.CustomerSummary.Domain.Common.Constants;
using Zip.Api.CustomerSummary.Domain.Entities.Countries;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Countries.Query.GetCountries
{
    public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, IEnumerable<Country>>
    {
        private readonly ICountryContext _countryContext;
        private readonly IMemoryCache _cache;

        public GetCountriesQueryHandler(ICountryContext countryContext, IMemoryCache memoryCache)
        {
            _countryContext = countryContext ?? throw new ArgumentNullException(nameof(countryContext));
            _cache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public async Task<IEnumerable<Country>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
        {
            if (!_cache.TryGetValue(CacheKeys.Products, out IEnumerable<Country> countries))
            {
                countries = await _countryContext.GetCountriesAsync();
                if (countries.IsNotEmpty())
                {
                    _cache.Set(CacheKeys.Products, countries);
                }
            }

            return countries;
        }
    }
}
