using System;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.PayNowUrlGenerator;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts
{
    public class PayNowAccountContext : IPayNowAccountContext
    {
        private readonly IDbContext _dbContext;
        public PayNowAccountContext(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<PayNowLinkAccount> GetPayNowLinkAccountAsync(long accountId)
        {
            var sql = @"
SELECT [A].Id AccountId,[AT].[Classification],P.CountryId  
  FROM AccountType [AT] 
 INNER JOIN Account A ON [AT].Id = A.AccountTypeId 
 INNER JOIN Product P ON [AT].ProductId = P.Id
 WHERE A.Id = @accountId
";

            return await _dbContext.QueryFirstOrDefaultAsync<PayNowLinkAccount>(sql, new { accountId });
        }
    }
}
