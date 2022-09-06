using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Statement;
using Zip.Api.CustomerSummary.Domain.Entities.Statements;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts
{
    public class StatementContext : IStatementContext
    {
        private readonly IDbContext _dbContext;

        public StatementContext(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task CreateAsync(Statement statement)
        {
            const string sql = @"
INSERT INTO [Statement]
           ([ConsumerId]
           ,[Name]
           ,[From]
           ,[To]
           ,[OpeningBalance]
           ,[ClosingBalance]
           ,[InterestFreeBalance]
           ,[AvailableFunds]
           ,[TimeStamp])
     VALUES
           (@ConsumerId
           ,@Name
           ,@From
           ,@To
           ,@OpeningBalance
           ,@ClosingBalance
           ,@InterestFreeBalance
           ,@AvailableFunds
           ,GETDATE())
";
            
            await _dbContext.ExecuteAsync(sql, statement);
        }

        public async Task<Statement> GetAsync(string statementName)
        {
            const string sql = @"
SELECT TOP 1 * 
  FROM [Statement] (NOLOCK) 
 WHERE [Name]=@statementName
";

            return await _dbContext.QueryFirstOrDefaultAsync<Statement>(sql, new { statementName });
        }

        public async Task<IEnumerable<Statement>> GetAsync(long consumerId)
        {
            const string sql = @"
SELECT *
  FROM [Statement] (NOLOCK) 
 WHERE ConsumerId=@consumerId
";
                    
            return await _dbContext.QueryAsync<Statement>(sql, new { consumerId });
        }

        public async Task<Statement> GetAsync(long consumerId, DateTime statementDate)
        {
            const string sql = @"
SELECT TOP 1 * 
  FROM [Statement] (NOLOCK) 
 WHERE ConsumerId=@consumerId 
   AND CAST([To] as date) = CAST(@statementDate as date)
";
            
            return await _dbContext.QueryFirstOrDefaultAsync<Statement>(sql, new { consumerId, statementDate });
        }
        
        public async Task<IEnumerable<StatementDate>> GetStatementDatesAsync(long accountId)
        {
            const string sql = @"
SELECT StatementDate 
  FROM Account 
 WHERE Id=@accountId
";

            var statementDates = new List<StatementDate>();

            var statementDate = await _dbContext.ExecuteScalarAsync<DateTime?>(sql, new { accountId });
            
            if (statementDate.HasValue)
            {
                var startDate = statementDate.Value;
                var endDate = startDate.AddMonths(1);

                while (endDate.Date < DateTime.Now.Date)
                {
                    statementDates.Add(new StatementDate(startDate, endDate));
                    startDate = startDate.AddMonths(1);
                    endDate = startDate.AddMonths(1);
                }
            }

            return statementDates.OrderByDescending(x => x.EndDate);
        }

        public async Task UpdateAsync(Statement statement)
        {
            const string sql = @"
UPDATE [Statement]
   SET [From] = @From
      ,[To] = @To
      ,[OpeningBalance] = @OpeningBalance
      ,[ClosingBalance] = @ClosingBalance
      ,[InterestFreeBalance] = @InterestFreeBalance
      ,[AvailableFunds] = @AvailableFunds
      ,[TimeStamp] = GETDATE() 
 WHERE Id=@Id
";
            
            await _dbContext.ExecuteAsync(sql, statement);
        }
    }
}
