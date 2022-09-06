using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Domain.Entities.MessageLog;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts
{
    public class MessageLogContext : IMessageLogContext
    {
        private readonly IDbContext _dbContext;

        public MessageLogContext(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<MessageLog> GetAsync(long id)
        {
            const string sql = @"
SELECT *
FROM   [MessageLog]
WHERE  Id = @id
";
            return await _dbContext.QueryFirstOrDefaultAsync<MessageLog>(sql, new { id });
        }

        public async Task<IEnumerable<MessageLog>> GetEmailsSentAsync(long consumerId, List<int> type)
        {
            var sql = @"SELECT * FROM   [MessageLog] where category = 1 and referenceid =@consumerId and type in (" + String.Join(",", type) +");";
            return await _dbContext.QueryAsync<MessageLog>(sql, new { consumerId });
        }

        public async Task<IEnumerable<MessageLogDto>> GetAsync(MessageLogCategory category, long referenceId)
        {
            const string sql = @"
SELECT   ml.*,
         mle.*
FROM     MessageLog ml
            JOIN MessageLogEvent mle ON mle.MessageLogId = ml.id
WHERE    mle.Id = (SELECT   TOP (1) Id
                   FROM     MessageLogEvent
                   WHERE    MessageLogId = mle.MessageLogId
                   ORDER BY TimeStamp DESC)
AND      ml.Category    = @category
AND      ml.ReferenceId = @referenceId
ORDER BY ml.TimeStamp DESC
";

            using (var conn = _dbContext.GetDbConnection())
            {
                var messageLogs = await conn.QueryAsync<MessageLog, MessageLogEvent, MessageLogDto>(
                    sql,
                    (messageLog, messageLogEvent) => new MessageLogDto
                    {
                        MessageLog = messageLog,
                        MessageLogEvent = messageLogEvent
                    },
                    new
                    {
                        category,
                        referenceId
                    },
                    splitOn: "Id,Id");

                return messageLogs;
            }
        }

        public async Task<bool> InsertAsync(
            long referenceId,
            Guid messageId,
            string subject,
            string detail,
            MessageLogSettings settings,
            DateTime timeStamp,
            string messageBody = null)
        {
            const string insertMessageLogSql = @"
INSERT INTO MessageLog 
	(	
		DeliveryMethod,
		Category,
		[Type],
		ReferenceId,
		[Subject],
		[TimeStamp],
		MessageId,
		Content
	) 
VALUES 
	(
		@deliveryMethod,
		@category,
		@type,
		@referenceId,
		@subject,
		getdate(),
		@messageId,
		@content
	);

SELECT CAST(SCOPE_IDENTITY() AS int);
";


            const string insertMessageLogEventSql = @"
INSERT INTO MessageLogEvent
    (
        [MessageLogId],
        [Status],
        [Detail],
        [TimeStamp]
    )
VALUES 
    (
        @messageLogId,
        @status,
        @detail,
        @timestamp
    );

SELECT CAST(SCOPE_IDENTITY() AS int);
";

            var messageLogId = await _dbContext.ExecuteScalarAsync<long>(insertMessageLogSql,
                new
                {
                    deliveryMethod = settings.DeliveryMethod,
                    category = settings.Category,
                    type = settings.Type,
                    referenceId,
                    subject,
                    messageId,
                    content = messageBody
                });

            var messageLog = await GetAsync(messageLogId);

            var messageLogEventId = await _dbContext.ExecuteScalarAsync<long>(insertMessageLogEventSql,
                new
                {
                    messageLogId = messageLog.Id,
                    status = settings.Status,
                    detail,
                    timestamp = messageLog.TimeStamp
                });

            return messageLogEventId > 0;
        }
    }
}
