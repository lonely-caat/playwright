using System;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts
{
    public class AccountTypeContext : IAccountTypeContext
    {
        private readonly IDbContext _dbContext;

        public AccountTypeContext(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<AccountType> GetAsync(long id)
        {
            const string sql = @"
SELECT * 
FROM   AccountType (NOLOCK) 
WHERE  Id = @id
";
            return await _dbContext.QuerySingleOrDefaultAsync<AccountType>(sql, new { id });
        }
    }
}
