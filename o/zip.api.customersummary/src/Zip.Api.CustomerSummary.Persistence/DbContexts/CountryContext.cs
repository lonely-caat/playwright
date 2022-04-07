using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Countries;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts
{
    public class CountryContext : ICountryContext
    {
        private readonly IDbContext _dbContext;

        public CountryContext(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Country> GetAsync(string countryId)
        {
            var sql = @"
SELECT [Id],[Name]  
  FROM Country  
 WHERE [Id]=@countryId
";

            return await _dbContext.QueryFirstOrDefaultAsync<Country>(sql, new { countryId });
        }

        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            var sql = @"
SELECT [Id],[Name] FROM Country
";
            return await _dbContext.QueryAsync<Country>(sql);
        }
    }
}
