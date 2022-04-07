using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Infrastructure.Services.StatementService;
using Zip.Api.CustomerSummary.Infrastructure.Services.StatementService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.StatementService.Models;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Services
{
    public class StatementsServiceTests : CommonTestsFixture
    {
        private readonly Mock<IStatementsApiProxy> _statementsApiProxy;

        private readonly StatementsService _target;
        
        public StatementsServiceTests()
        {
            _statementsApiProxy = new Mock<IStatementsApiProxy>();
            
            _target = new StatementsService(_statementsApiProxy.Object);
        }
        
        [Fact]
        public async Task Given_StatementsApiProxy_Response_Is_Ok_WhenGenerateStatementsAsync_Then_Response_Should_Be_Correct()
        {
            // Arrange
            var request = Fixture.Create<GenerateStatementsRequest>();
            
            _statementsApiProxy.Setup(x => x.TriggerStatementsGenerationAsync(request, CancellationToken))
                               .ReturnsAsync(new HttpResponseMessage
                                {
                                    StatusCode = HttpStatusCode.Created
                                });

            // Act
            var actual = await _target.GenerateStatementsAsync(request, CancellationToken);

            // Assert
            actual.Should().BeTrue();
        }

        [Fact]
        public async Task Given_StatementsApiProxy_Response_Has_Error_When_WhenGenerateStatementsAsync_Should_Be_Correct()
        {
            // Arrange
            var request = Fixture.Create<GenerateStatementsRequest>();

            _statementsApiProxy.Setup(x => x.TriggerStatementsGenerationAsync(request, CancellationToken))
                               .ReturnsAsync(new HttpResponseMessage
                                {
                                    StatusCode = HttpStatusCode.BadRequest
                                });

            // Act
            var actual = await _target.GenerateStatementsAsync(request, CancellationToken);

            // Assert
            actual.Should().BeFalse();
        }
    }
}