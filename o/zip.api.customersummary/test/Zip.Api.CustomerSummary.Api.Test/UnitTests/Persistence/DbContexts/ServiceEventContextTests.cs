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
    public class ServiceEventContextTests
    {
        private readonly ServiceEventContext _target;

        private readonly Fixture _fixture;

        private readonly Mock<IDbContext> _dbContext;

        public ServiceEventContextTests()
        {
            _fixture = new Fixture();
            _dbContext = new Mock<IDbContext>();

            _target = new ServiceEventContext(_dbContext.Object);
        }
        
        [Fact]
        public async Task GetAsync_Should_Invoke_DbContext_QueryFirstOrDefaultAsync_Correctly()
        {
            // Arrange
            const string expectSql = @"
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
            var expectMessageCategory = _fixture.Create<MessageCategory>();
            var expectMessageType = _fixture.Create<CustomerSummary.Domain.Entities.Statements.MessageTypes>();
            var expectReference = _fixture.Create<string>();
            var expectParams = new
            {
	            Category = expectMessageCategory,
	            Type = expectMessageType,
	            Reference = expectReference
            };
            
            // Action
            await _target.GetAsync(expectMessageCategory,
                                   expectMessageType,
                                   expectReference);

            // Assert
            _dbContext.Verify(
		            x => x.QueryFirstOrDefaultAsync<ServiceEvent>(
				            expectSql,
				            It.Is<object>(y => JsonConvert.SerializeObject(y) == JsonConvert.SerializeObject(expectParams))),
		            Times.Once);
        }

        [Fact]
        public async Task CreateAsync_Should_Invoke_DbContext_ExecuteAsync_Correctly()
        {
	        // Arrange
	        const string expectSql = @"
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
            var expectMessageId = _fixture.Create<Guid>();
            var expectMessageCategory = _fixture.Create<MessageCategory>();
	        var expectMessageType = _fixture.Create<CustomerSummary.Domain.Entities.Statements.MessageTypes>();
	        var expectReference = _fixture.Create<string>();
            var expectStatus = _fixture.Create<MessageStatus>();
            var expectParams = new
	        {
                MessageId = expectMessageId,
		        Category = expectMessageCategory,
		        Type = expectMessageType,
		        Reference = expectReference,
                Status = expectStatus
	        };

	        // Action
	        await _target.CreateAsync(expectMessageId,
	                                  expectMessageCategory,
	                                  expectMessageType,
	                                  expectReference,
                                      expectStatus);

	        // Assert
	        _dbContext.Verify(
			        x => x.ExecuteAsync(
					        expectSql,
					        It.Is<object>(y => JsonConvert.SerializeObject(y) == JsonConvert.SerializeObject(expectParams))),
			        Times.Once);
        }
    }
}
