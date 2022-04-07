using System;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Newtonsoft.Json;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Statements;
using Zip.Api.CustomerSummary.Persistence.DbContexts;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Persistence.DbContexts
{
    public class StatementContextTests
    {
        private readonly StatementContext _target;

        private readonly Fixture _fixture;

        private readonly Mock<IDbContext> _dbContext;

        public StatementContextTests()
        {
            _fixture = new Fixture();
            _dbContext = new Mock<IDbContext>();

            _target = new StatementContext(_dbContext.Object);
        }
        
        [Fact]
        public async Task When_CreateAsync_Should_Invoke_DbContext_ExecuteAsync_Correctly()
        {
            // Arrange
            const string expectSql = @"
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
            var statement = _fixture.Create<Statement>();
            
            // Action
            await _target.CreateAsync(statement);

            // Assert
            _dbContext.Verify(x => x.ExecuteAsync(expectSql, statement), Times.Once);
        }
        
        [Fact]
        public async Task Given_StatementName_When_GetAsync_Should_Invoke_DbContext_QueryFirstOrDefaultAsync_Correctly()
        {
            // Arrange
            const string expectSql = @"
SELECT TOP 1 * 
  FROM [Statement] (NOLOCK) 
 WHERE [Name]=@statementName
";
            var expectStatementName = _fixture.Create<Guid>().ToString();
            var expectParams = new { statementName = expectStatementName };

            // Action
            await _target.GetAsync(expectStatementName);

            // Assert
            _dbContext.Verify(
                    x => x.QueryFirstOrDefaultAsync<Statement>(
                            expectSql,
                            It.Is<object>(y => JsonConvert.SerializeObject(y) == JsonConvert.SerializeObject(expectParams))),
                    Times.Once);
        }

        [Fact]
        public async Task Given_ConsumerId_When_GetAsync_Should_Invoke_DbContext_QueryAsync_Correctly()
        {
            // Arrange
            const string expectSql = @"
SELECT *
  FROM [Statement] (NOLOCK) 
 WHERE ConsumerId=@consumerId
";
            var expectConsumerId = _fixture.Create<long>();
            var expectParams = new { consumerId = expectConsumerId };

            // Action
            await _target.GetAsync(expectConsumerId);

            // Assert
            _dbContext.Verify(
                    x => x.QueryAsync<Statement>(
                            expectSql,
                            It.Is<object>(y => JsonConvert.SerializeObject(y) == JsonConvert.SerializeObject(expectParams))),
                    Times.Once);
        }
        
        [Fact]
        public async Task Given_ConsumerId_And_StatementDate_When_GetAsync_Should_Invoke_DbContext_QueryFirstOrDefaultAsync_Correctly()
        {
            // Arrange
            const string expectSql = @"
SELECT TOP 1 * 
  FROM [Statement] (NOLOCK) 
 WHERE ConsumerId=@consumerId 
   AND CAST([To] as date) = CAST(@statementDate as date)
";
            var expectConsumerId = _fixture.Create<long>();
            var expectStatementDate = _fixture.Create<DateTime>();
            var expectParams = new
            {
                consumerId = expectConsumerId,
                statementDate = expectStatementDate
            };

            // Action
            await _target.GetAsync(expectConsumerId, expectStatementDate);

            // Assert
            _dbContext.Verify(
                    x => x.QueryFirstOrDefaultAsync<Statement>(
                            expectSql,
                            It.Is<object>(y => JsonConvert.SerializeObject(y) == JsonConvert.SerializeObject(expectParams))),
                    Times.Once);
        }

        [Fact]
        public async Task Given_AccountId_When_GetStatementDatesAsync_Should_Invoke_DbContext_ExecuteScalarAsync_Correctly()
        {
            // Arrange
            const string expectSql = @"
SELECT StatementDate 
  FROM Account 
 WHERE Id=@accountId
";
            var expectAccountId = _fixture.Create<long>();
            var expectParams = new { accountId = expectAccountId };

            // Action
            await _target.GetStatementDatesAsync(expectAccountId);

            // Assert
            _dbContext.Verify(
                    x => x.ExecuteScalarAsync<DateTime?>(
                            expectSql,
                            It.Is<object>(y => JsonConvert.SerializeObject(y) == JsonConvert.SerializeObject(expectParams))),
                    Times.Once);
        }
        
        [Fact]
        public async Task When_UpdateAsync_Should_Invoke_DbContext_ExecuteAsync_Correctly()
        {
            // Arrange
            const string expectSql = @"
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
            var statement = _fixture.Create<Statement>();

            // Action
            await _target.UpdateAsync(statement);

            // Assert
            _dbContext.Verify(x => x.ExecuteAsync(expectSql, statement), Times.Once);
        }
    }
}
