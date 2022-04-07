using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Countries;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces
{
    public interface ICountryContext
    {
        Task<IEnumerable<Country>> GetCountriesAsync();

        Task<Country> GetAsync(string countryId);
    }
}