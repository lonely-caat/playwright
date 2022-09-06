using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Products;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts
{
    public class ProductContext : IProductContext
    {
        private readonly IDbContext _dbContext;

        public ProductContext()
        {
        }
        
        public ProductContext(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<Product>> GetAsync()
        {
            var query = @"
SELECT  Id,
        Classification,
        CountryId,
        Status 
  FROM [Product] (NOLOCK) 
 WHERE Status=1";

            return await _dbContext.QueryAsync<Product>(query);
        }

        public async Task<Product> GetAsync(long id)
        {
            var query = @"
SELECT  TOP 1 
        Id,
        Classification,
        CountryId,
        Status 
  FROM [Product] (NOLOCK) 
 WHERE Id=@id";


            return await _dbContext.QueryFirstOrDefaultAsync<Product>(query, new { id });
        }
    }
}
