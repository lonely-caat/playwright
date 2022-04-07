using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService;
using Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Models;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Services
{
    public class CommunicationsServiceTests : CommonTestsFixture
    {
        private readonly Mock<ICommunicationsServiceProxy> _communicationServiceProxy;
        private readonly Mock<ILogger<CommunicationsService>> _logger;
        private readonly CommunicationsService _service;

        public CommunicationsServiceTests()
        {
            _communicationServiceProxy = new Mock<ICommunicationsServiceProxy>();
            _logger = new Mock<ILogger<CommunicationsService>>();
            _service = new CommunicationsService(_communicationServiceProxy.Object, _logger.Object);
        }

        [Fact]
        public void Given_NullInjection_Should()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new CommunicationsService(null, _logger.Object);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                new CommunicationsService(_communicationServiceProxy.Object, null);
            });
        }

        [Fact]
        public async Task Given_Valid_Input_Should_Invoke_Correctly()
        {
            var request = Fixture.Create<PaidOutAndClosedEmail>();
            var response = Fixture.Create<CommunicationApiResponse>();
            response.Success = true;

            _communicationServiceProxy.Setup(x => x.SendPaidOutAndClosedEmailAsync(It.IsAny<PaidOutAndClosedEmail>()))
                        .Returns(Task.FromResult(response));

           CommunicationApiResponse result = await _service.SendPaidOutCloseEmailAsync(request);

           Assert.True(result.Success);
        }

        [Fact]
        public async Task Given_InValid_Input_Should_Throw_Exception()
        {
            var request = Fixture.Create<PaidOutAndClosedEmail>();

            _communicationServiceProxy.Setup(x => x.SendPaidOutAndClosedEmailAsync(It.IsAny<PaidOutAndClosedEmail>()))
                        .ThrowsAsync(new Exception());

            await Assert.ThrowsAsync<Exception>(() => _service.SendPaidOutCloseEmailAsync(request));
        }

        [Fact]
        public async Task Given_ValidResponse_ResetPassword_Should_Return_CommunicationApiResponse()
        {
            // Arrange
            var request = Fixture.Create<ResetPassword>();
            var response = Fixture.Create<CommunicationApiResponse>();
            _communicationServiceProxy.Setup(x => x.SendResetPasswordEmailAsync(It.IsAny<ResetPassword>()))
                .ReturnsAsync(response);

            // Act
            var actual =  await _service.SendResetPasswordAsync(request);

            // Assert
            Assert.Equal(response.Message, actual.Message);
            Assert.Equal(response.Success, response.Success);
        }

        [Fact]
        public async Task Given_InvalidResponse_ResetPassword_Should_Throw_Exception()
        {
            // Arrange
            var request = Fixture.Create<ResetPassword>();
            _communicationServiceProxy.Setup(x => x.SendResetPasswordEmailAsync(It.IsAny<ResetPassword>()))
                .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAnyAsync<Exception>(async () => await _service.SendResetPasswordAsync(request));
        }
    }
}