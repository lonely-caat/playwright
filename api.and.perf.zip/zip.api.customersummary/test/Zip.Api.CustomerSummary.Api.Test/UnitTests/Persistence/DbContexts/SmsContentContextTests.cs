using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Newtonsoft.Json;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Communications;
using Zip.Api.CustomerSummary.Persistence.DbContexts;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Persistence.DbContexts
{
    public class SmsContentContextTests
    {
        private readonly SmsContentContext _target;
        
        private readonly Fixture _fixture;

        private readonly Mock<IDbContext> _dbContext;

        public SmsContentContextTests()
        {
            _fixture = new Fixture();
            _dbContext = new Mock<IDbContext>();
            
            _target = new SmsContentContext(_dbContext.Object);
        }
        
        [Fact]
        public async Task Given_Name_When_GetAsync_Should_Invoke_DbContext_QueryFirstOrDefaultAsync_Correctly()
        {
            // Arrange
            const string expectSql = @"
SELECT *
  FROM SmsContent
 WHERE [Name]=@name
   AND Active = 1
";
            var expectName = _fixture.Create<string>();
            var expectParams = new { name = expectName };
            
            // Action
            await _target.GetAsync(expectName);

            // Assert
            _dbContext.Verify(
                    x => x.QueryFirstOrDefaultAsync<SmsContent>(
                            expectSql,
                            It.Is<object>(y => JsonConvert.SerializeObject(y) == JsonConvert.SerializeObject(expectParams))),
                    Times.Once);
        }
        
        [Fact]
        public async Task Given_Id_When_GetAsync_Should_Invoke_DbContext_QueryFirstOrDefaultAsync_Correctly()
        {
            // Arrange
            
            const string expectSql = @"
SELECT *
  FROM SmsContent
 WHERE [Id]=@id
";
            var expectId = _fixture.Create<int>();
            var expectParams = new { id = expectId };
            
            // Action
            await _target.GetAsync(expectId);

            // Assert
            _dbContext.Verify(
                    x => x.QueryFirstOrDefaultAsync<SmsContent>(
                            expectSql,
                            It.Is<object>(y => JsonConvert.SerializeObject(y) == JsonConvert.SerializeObject(expectParams))),
                    Times.Once);
        }
    }
}
