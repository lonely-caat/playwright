using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Transactions;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts
{
    public class TransactionHistoryContext : ITransactionHistoryContext
    {
        private readonly IDbContext _dbContext;

        public TransactionHistoryContext(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<TransactionHistory>> FindByConsumerIdAsync(long consumerId, DateTime startDate, DateTime endDate)
        {
            const string sql = @"
SELECT sh.Status, MAX(th.[type]) as Type, sh.Total as Amount, MIN(th.[TimeStamp]) as TimeStamp, mer.Name as MerchantName, th.OrderId
    FROM dbo.TransactionHistory (NOLOCK) th
    JOIN dbo.[Order] ord ON ord.Id = th.OrderId
    JOIN dbo.OperationRequest op ON op.Id = ord.OperationRequestId
    JOIN dbo.ShoppingCartDetail sh ON sh.OperationRequestId = op.Id
    JOIN dbo.Merchant mer ON sh.MerchantId= mer.Id
WHERE ConsumerId = @consumerId
    AND th.[TimeStamp] BETWEEN CAST(@startDate as datetime) AND CAST(@endDate as datetime)
    AND th.[Type] in (1,2,3,5)
GROUP BY th.OrderId, mer.Name, sh.Total, sh.Status
ORDER BY MIN(th.[TimeStamp]) DESC
";
            
            return await _dbContext.QueryAsync<TransactionHistory>(sql, new { consumerId, startDate, endDate });
        }

        public async Task<IEnumerable<TransactionHistory>> FindByAccountIdAsync(long accountId, DateTime startDate, DateTime endDate)
        {
            // Exclude MerchantTransaction Types
            const string sql = @"
SELECT    [Id],
          [AccountId],
          [ConsumerId],
          [ThreadId],
          [OrderId],
          [Type],
          [TimeStamp],
          [Amount]
FROM      [TransactionHistory] (NOLOCK)
WHERE     [AccountId] = @accountId
AND       [TimeStamp] BETWEEN CAST(@startDate as datetime) AND CAST(@endDate as datetime)
AND       [Type] NOT IN (7, 9, 30, 33, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49)
ORDER BY  [TimeStamp] DESC
";

            return await _dbContext.QueryAsync<TransactionHistory>(sql, new { accountId, startDate, endDate });
        }
    }
}
