using System;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts
{
    public class MerchantContext : IMerchantContext
    {
        private readonly IDbContext _dbContext;

        public MerchantContext(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Merchant> GetAsync(long id)
        {
            const string sql = @"
SELECT * 
  FROM Merchant 
 WHERE Id = @id
";

            return await _dbContext.QueryFirstOrDefaultAsync<Merchant>(sql, new { id });
        }
    }
}
