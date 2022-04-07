using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetLoginStatus;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Services.CustomerCoreService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CustomerCoreService.Models;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Consumers
{
    public class GetLoginStatusQueryHandlerTest : CommonTestsFixture
    {
        private readonly GetLoginStatusQueryHandler _target;
        
        private readonly Mock<ILogger<GetLoginStatusQueryHandler>> _logger;

        private readonly Mock<ICustomerCoreService> _customerCoreService;
        
        public GetLoginStatusQueryHandlerTest()
        {
            _logger = new Mock<ILogger<GetLoginStatusQueryHandler>>();
            _customerCoreService = new Mock<ICustomerCoreService>();
            _target = new GetLoginStatusQueryHandler(_logger.Object, _customerCoreService.Object);
        }
        
        [Fact]
        public async Task Given_Valid_GetLoginRequest_Should_Return_LoginStatusResponse()
        {
            // Arrange
            var request = Fixture.Build<GetLoginStatusQuery>()
                                 .With(x => x.ConsumerEmail, "johnny.vuong@zip.co")
                                 .Create();
            var mockResponse = Fixture.Create<GetLoginStatusResponse>();

            _customerCoreService.Setup(x => x.GetCustomerLoginStatusAsync(It.IsAny<GetLoginStatusRequest>(), It.IsAny<CancellationToken>()))
                                .ReturnsAsync(mockResponse);
            
            // Act
            var response = await _target.Handle(request, default);

            // Assert
            Assert.Equal(mockResponse.Data.LoginStatus, response.LoginStatus);
            _customerCoreService.Verify(x => x.GetCustomerLoginStatusAsync(It.Is<GetLoginStatusRequest>(x => x.Email == request.ConsumerEmail), default), Times.Once);
        }

        [Fact]
        public async Task Given_Invalid_GetLoginRequest_Should_Return_LoginStatusResponse()
        {
            // Arrange
            var request = Fixture.Build<GetLoginStatusQuery>()
                                 .With(x => x.ConsumerEmail, "johnny.vuong@zip.co")
                                 .Create();

            _customerCoreService.Setup(x => x.GetCustomerLoginStatusAsync(It.IsAny<GetLoginStatusRequest>(), It.IsAny<CancellationToken>()))
                                .ThrowsAsync(new CustomerCoreApiException());

            // Act & Assert
            await Assert.ThrowsAsync<CustomerCoreApiException>(
                async () => await _target.Handle(request, default));
        }
    }
}
