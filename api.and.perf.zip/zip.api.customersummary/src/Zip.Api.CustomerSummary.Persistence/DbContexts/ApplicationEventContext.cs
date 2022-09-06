using System;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Events;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts
{
    public class ApplicationEventContext : IApplicationEventContext
    {
        private readonly IDbContext _dbContext;

        public ApplicationEventContext(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }


        public async Task<ApplicationEvent> CreateAsync(ApplicationEvent @event)
        {
            var insertSql = "INSERT INTO [Events].[ApplicationEvents] (EventId,AggregateType,AggregateId,EventType,Payload,CreatedTimestamp,Published) " +
                "VALUES (@EventId,@AggregateType,@AggregateId,@EventType,@Payload,Getdate(),1);SELECT CAST(SCOPE_IDENTITY() as int);";


            var eventId = await _dbContext.ExecuteScalarAsync<long>(insertSql, @event);
            return await GetAsync(eventId);
        }

        public async Task<ApplicationEvent> GetAsync(long eventId)
        {
            var sql = @"
SELECT * 
  FROM [Events].[ApplicationEvents] (NOLOCK) 
 WHERE Id=@eventId";

            return await _dbContext.QueryFirstOrDefaultAsync<ApplicationEvent>(sql, new { eventId });
        }

        public async Task MarkAsUnpublishedAsync(long eventId)
        {
            var updateSql = "UPDATE [Events].[ApplicationEvents] SET Published=0 WHERE Id=@eventId";

            await _dbContext.ExecuteAsync(updateSql, new { eventId });
        }
    }
}
