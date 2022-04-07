using System;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts
{
    public class ConsumerStatContext : IConsumerStatContext
    {
        private readonly IDbContext _dbContext;

        public ConsumerStatContext(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<ConsumerStat> GetAsync(long id)
        {
            var sql = @"
SELECT * 
  FROM [ConsumerStats]
 WHERE [Id]=@id
";

            return await _dbContext.QuerySingleOrDefaultAsync<ConsumerStat>(sql, new { id });
        }

        public async Task<ConsumerStat> GetByConsumerIdAsync(long consumerId)
        {
            const string sql = @"
SELECT * 
  FROM [ConsumerStats]
 WHERE ConsumerId=@consumerId
";

            return await _dbContext.QueryFirstOrDefaultAsync<ConsumerStat>(sql, new { consumerId });
        }

        public async Task SaveAsync(ConsumerStat consumerStat)
        {
            const string sql = @"
BEGIN TRAN

IF EXISTS (SELECT * FROM [ConsumerStats] WHERE [Id] = @Id)
BEGIN
  UPDATE [ConsumerStats] 
     SET SmsSendRetryCount=@SmsSendRetryCount,
         SendTimeStamp=GETDATE()
   WHERE [Id]=@Id
END
ELSE
BEGIN
  INSERT INTO [ConsumerStats] 
    ( ConsumerId, SmsVerificationRetryCount, SmsSendRetryCount, VerificationTimeStamp, SendTimeStamp)
  VALUES
    ( @ConsumerId, 0, 0, GETDATE(), GETDATE())
END

COMMIT TRAN


";
            await _dbContext.ExecuteAsync(sql, consumerStat);
        }
    }
}
