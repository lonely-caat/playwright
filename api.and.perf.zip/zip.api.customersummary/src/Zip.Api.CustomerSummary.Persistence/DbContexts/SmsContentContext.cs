using System;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Communications;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts
{
    public class SmsContentContext : ISmsContentContext
    {
        private readonly IDbContext _dbContext;

        public SmsContentContext(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<SmsContent> GetAsync(string name)
        {
            const string sql = @"
SELECT *
  FROM SmsContent
 WHERE [Name]=@name
   AND Active = 1
";

            return await _dbContext.QueryFirstOrDefaultAsync<SmsContent>(sql, new { name });
        }

        public async Task<SmsContent> GetAsync(int id)
        {
            const string sql = @"
SELECT *
  FROM SmsContent
 WHERE [Id]=@id
";

            return await _dbContext.QueryFirstOrDefaultAsync<SmsContent>(sql, new { id });
        }
    }
}
