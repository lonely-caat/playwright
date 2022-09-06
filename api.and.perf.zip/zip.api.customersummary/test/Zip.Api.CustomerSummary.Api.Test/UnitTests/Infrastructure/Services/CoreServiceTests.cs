using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Refit;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.UnitTests.Common;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreService;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreService.Models;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Services
{
    public class CoreServiceTests : CommonTestFixture
    {
        private readonly Mock<IOptions<CoreApiProxyOptions>> _coreApiProxyOptions;

        private readonly Mock<ICoreServiceProxy> _coreServiceProxy;

        private readonly Mock<ILogger<CoreService>> _logger;

        private readonly ICoreService _target;

        public CoreServiceTests()
        {
            _coreServiceProxy = new Mock<ICoreServiceProxy>();
            _logger = new Mock<ILogger<CoreService>>();
            _coreApiProxyOptions = new Mock<IOptions<CoreApiProxyOptions>>();

            var mockOptions = _fixture.Build<CoreApiProxyOptions>()
                                      .With(x => x.ClientSecret, "test")
                                      .With(x => x.ClientId, "test")
                                      .Create();
            var emailOptions = Options.Create(mockOptions);

            _coreApiProxyOptions.Setup(x => x.Value)
                                .Returns(emailOptions.Value);

            _target = new CoreService(_coreServiceProxy.Object,
                                      _logger.Object,
                                      _coreApiProxyOptions.Object);
        }

        [Fact]
        public async Task Given_400ErrorOccurs_ResetPasswordEmail_Should_Return_CoreApiException()
        {
            // Arrange
            var coreResetPassword = _fixture.Create<CoreResetPasswordModel>();
            _coreServiceProxy.Setup(x => x.SendResetPasswordEmailAsync(
                                        It.Is<CoreResetPasswordModel>(y => y.Email == coreResetPassword.Email),
                                        "token",
                                        CancellationToken.None))
                             .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest));

            // Act & Assert
            await Assert.ThrowsAsync<CoreApiException>(
                async () => await _target.SendResetPasswordEmailAsync(coreResetPassword, "token", CancellationToken.None));
        }

        [Fact]
        public async Task Given_ExceptionOccurs_GetCoreTokenAsync_Should_Return_CoreApiException()
        {
            var dictionary = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", "test" },
                { "client_secret", "test" }
            };

            var exception = await ApiException.Create(
                                new HttpRequestMessage(),
                                HttpMethod.Post,
                                new HttpResponseMessage(HttpStatusCode.Unauthorized));

            _coreServiceProxy.Setup(x => x.GetTokenAsync(dictionary, CancellationToken.None))
                             .ThrowsAsync(exception);

            // Act & Assert
            await Assert.ThrowsAsync<CoreApiException>(async () => await _target.GetCoreTokenAsync(CancellationToken.None));
        }

        [Fact]
        public async Task Given_TokenReturns_GetCoreTokenAsync_Should_Return_Model()
        {
            // Arrange
            var expectedResponse = _fixture.Create<GetCoreTokenResponse>();
            var dictionary = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", "test" },
                { "client_secret", "test" }
            };

            _coreServiceProxy.Setup(x => x.GetTokenAsync(dictionary, CancellationToken.None))
                             .ReturnsAsync(expectedResponse);

            // Act
            var response = await _target.GetCoreTokenAsync(CancellationToken.None);

            // Assert
            Assert.Equal(expectedResponse.ToJsonString(), response.ToJsonString());
        }

        [Fact]
        public async Task Given_TokenReturns_ResetPasswordEmail_Should_Return_200()
        {
            // Arrange
            var coreResetPassword = _fixture.Create<CoreResetPasswordModel>();
            _coreServiceProxy.Setup(x => x.SendResetPasswordEmailAsync(
                                        It.Is<CoreResetPasswordModel>(y => y.Email == coreResetPassword.Email),
                                        "token",
                                        CancellationToken.None))
                             .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.Accepted));

            // Act
            var response = await _target.SendResetPasswordEmailAsync(coreResetPassword, "token", CancellationToken.None);

            // Assert
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}