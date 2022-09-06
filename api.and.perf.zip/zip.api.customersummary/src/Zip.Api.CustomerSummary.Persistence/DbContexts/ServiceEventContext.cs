using System;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Statements;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts
{
    public class ServiceEventContext : IServiceEventContext
    {
        private readonly IDbContext _dbContext;

        public ServiceEventContext(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<ServiceEvent> GetAsync(MessageCategory category, MessageTypes type, string reference)
        {
            const string sql = @"
SELECT
	Id,
	MessageId,
	[Type],
	Reference,
	Category,
	[Status],
	[TimeStamp],
	[Result]
FROM ServiceEvent
WHERE Category = @category
	AND [Type] = @type
	AND Reference = @reference
ORDER BY ID DESC
";

            return await _dbContext.QueryFirstOrDefaultAsync<ServiceEvent>(sql, new { Category = category, Type = type, Reference = reference });
        }

        public async Task CreateAsync(
                Guid messageId,
                MessageCategory category,
                MessageTypes type,
                string reference,
                MessageStatus status)
        {
            const string sql = @"
INSERT INTO [ServiceEvent] 
    (MessageId
    ,[Type]
    ,[Reference]
    ,[Category]
    ,[Status]
    ,[TimeStamp]) 
VALUES 
    (@MessageId
    ,@Type
    ,@Reference
    ,@Category
    ,@Status
    ,GETDATE())
";
            await _dbContext.ExecuteAsync(sql, new { MessageId = messageId, Category = category, Type = type, Reference = reference, Status = status });
        }
    }
}
