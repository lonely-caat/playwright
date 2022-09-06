using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Countries;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.Mocks.DbContexts
{
    public class MockCountryContext : ICountryContext
    {
        private readonly List<Country> _countries;

        public MockCountryContext()
        {
            _countries = new List<Country>
            {
                new Country
                {
                    Id="Au",
                    Name="Australia"
                },
                new Country
                {
                    Id="NZ",
                    Name="New Zealand"
                }
            };
        }

        public Task<Country> GetAsync(string countryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Country>> GetCountriesAsync()
        {
            return Task.FromResult<IEnumerable<Country>>(_countries);
        }
    }
}
