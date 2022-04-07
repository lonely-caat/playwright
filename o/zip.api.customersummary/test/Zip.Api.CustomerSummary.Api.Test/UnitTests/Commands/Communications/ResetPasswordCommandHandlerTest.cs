using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.UnitTests.Common;
using Zip.Api.CustomerSummary.Application.Communications.Command.SendResetPasswordEmailNew;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreService.Models;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Communications
{
    public class ResetPasswordCommandHandlerTest : CommonTestFixture
    {
        private readonly Mock<ICoreService> _coreService;

        private readonly Mock<ILogger<SendResetPasswordEmailNewCommandHandler>> _logger;

        private readonly SendResetPasswordEmailNewCommandHandler _target;

        public ResetPasswordCommandHandlerTest()
        {
            _coreService = new Mock<ICoreService>();
            _logger = new Mock<ILogger<SendResetPasswordEmailNewCommandHandler>>();
            _target = new SendResetPasswordEmailNewCommandHandler(_coreService.Object, _logger.Object);
        }

        [Fact]
        public async Task Given_GetTokenThrowsAnException_Handle_ShouldThrowAnException()
        {
            // Arrange
            var request = _fixture.Create<SendResetPasswordEmailNewCommand>();
            _coreService.Setup(x => x.GetCoreTokenAsync(CancellationToken.None))
                        .ThrowsAsync(new CoreApiException("Failed to get token"));

            // Act & Assert
            await Assert.ThrowsAsync<CoreApiException>(async () => await _target.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Given_ResetPasswordIsSuccessful_Handle_ShouldReturnUnitValue()
        {
            // Arrange
            var request = _fixture.Create<SendResetPasswordEmailNewCommand>();
            var tokenResponse = _fixture.Create<GetCoreTokenResponse>();
            var expectedTokenResponse = $"{tokenResponse.TokenType} {tokenResponse.AccessToken}";
            var response = new HttpResponseMessage(HttpStatusCode.Accepted);
            _coreService.Setup(x => x.GetCoreTokenAsync(CancellationToken.None))
                        .ReturnsAsync(tokenResponse);
            _coreService.Setup(x => x.SendResetPasswordEmailAsync(
                                   It.Is<CoreResetPasswordModel>(y => y.Email == request.Email),
                                   expectedTokenResponse,
                                   CancellationToken.None))
                        .ReturnsAsync(response);
            
            // Act 
            var actual = await _target.Handle(request, CancellationToken.None);

            // Response
            Assert.Equal(Unit.Value, actual);
            _coreService.Verify(x => x.GetCoreTokenAsync(CancellationToken.None), Times.Once);
            _coreService.Verify(x => x.SendResetPasswordEmailAsync(
                                    It.Is<CoreResetPasswordModel>(y => y.Email == request.Email),
                                    expectedTokenResponse,
                                    CancellationToken.None),
                                Times.Once);
        }

        [Fact]
        public async Task Given_SendResetPasswordThrowsAnException_Handle_ShouldThrowAnException()
        {
            // Arrange
            var request = _fixture.Create<SendResetPasswordEmailNewCommand>();
            var tokenResponse = _fixture.Create<GetCoreTokenResponse>();
            var expectedTokenResponse = $"{tokenResponse.TokenType} {tokenResponse.AccessToken}";
            _coreService.Setup(x => x.GetCoreTokenAsync(CancellationToken.None))
                        .ReturnsAsync(tokenResponse);
            _coreService.Setup(x => x.SendResetPasswordEmailAsync(
                                   It.Is<CoreResetPasswordModel>(y => y.Email == request.Email),
                                   expectedTokenResponse,
                                   CancellationToken.None))
                        .ThrowsAsync(new CoreApiException("Failed to reset password"));

            // Act & Assert
            await Assert.ThrowsAsync<CoreApiException>(async () => await _target.Handle(request, CancellationToken.None));
        }
    }
}