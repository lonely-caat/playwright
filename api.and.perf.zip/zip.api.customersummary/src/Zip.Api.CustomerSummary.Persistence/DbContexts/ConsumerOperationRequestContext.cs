using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Dto.OrderActivity;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts
{
    public class ConsumerOperationRequestContext : IConsumerOperationRequestContext
    {
        private readonly IDbContext _dbContext;

        public ConsumerOperationRequestContext(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<OrderActivityDto>> GetOrderActivitiesAsync(long consumerId, DateTime fromDate, DateTime toDate)
        {
            const string sql = @"
SELECT  T1.[Timestamp] TimeStamp,
        T1.[OperationRequestId] Id,
        T2.[ParentOperationRequestId],
        T2.[Reference],
        T3.[Name] MerchantName,
        T2.[Type],
        T2.[Status],
        T2.[Message] Metadata
FROM    [ConsumerOperationRequest]   T1
        LEFT JOIN [OperationRequest] T2 ON T1.[OperationRequestId] = T2.[Id]
        LEFT JOIN [Merchant]         T3 ON T2.[MerchantId] = T3.[Id]
WHERE   T1.[ConsumerId] = @consumerId AND T1.[Timestamp] BETWEEN @fromDate AND @toDate
ORDER BY TimeStamp DESC
            ";

            return await _dbContext.QueryAsync<OrderActivityDto>(sql, new { consumerId, fromDate, toDate });
        }
    }
}
