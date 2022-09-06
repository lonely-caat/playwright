using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.Mfa.Query;
using Zip.Api.CustomerSummary.Domain.Common.Constants;
using Zip.Api.CustomerSummary.Domain.Entities.Mfa;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Services.MfaService;
using Zip.Api.CustomerSummary.Infrastructure.Services.MfaService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Services
{
    public class MfaServiceTests : CommonTestsFixture
    {
        private readonly MfaService _target;

        private readonly Mock<IMfaProxy> _mfaProxy;

        private readonly Mock<ILogger<MfaService>> _logger;

        public MfaServiceTests()
        {
            _mfaProxy = new Mock<IMfaProxy>();
            _logger = new Mock<ILogger<MfaService>>();
            _target = new MfaService(_mfaProxy.Object, _logger.Object);
        }
        [Fact]
        public async Task Given_Success_Response_From_MfaProxy_GetMfaSmsDataAsync_Should_Return_MfaSmsDataResponse()
        {
            // Arrange
            var request = Fixture.Build<GetMfaSmsDataQuery>()
                                .With(x => x.ConsumerId, 1234)
                                .Create();

            var response = Fixture.Build<MfaSmsDataResponse>()
                .With(x=> x.IsSuccess, true)
                .Create();

            _mfaProxy.Setup(x => x.GetMfaSmsDataAsync(It.IsAny<long>()))
                .ReturnsAsync(response);

            // Act
            var actual = await _target.GetMfaSmsDataAsync(request.ConsumerId);

            // Assert
            Assert.Equal(actual.IsSuccess, response.IsSuccess);

            _mfaProxy.Verify(x =>
                x.GetMfaSmsDataAsync(
                    It.Is<long>(y => y == request.ConsumerId)),
                Times.Once);
        }

        [Fact]
        public async Task Given_NotFound_RefitApiException_GetMfaSmsDataAsync_Should_Return_Null()
        {
            // Arrange
            var request = Fixture.Build<GetMfaSmsDataQuery>()
                .With(x => x.ConsumerId, 1234)
                .Create();

            var refitNotFoundException = await Refit.ApiException.Create(
                new HttpRequestMessage(),
                HttpMethod.Get,
                new HttpResponseMessage(HttpStatusCode.NotFound)
            );

            _mfaProxy.Setup(x => x.GetMfaSmsDataAsync(It.IsAny<long>()))
                .ThrowsAsync(refitNotFoundException);

            // Act
            var actual = await _target.GetMfaSmsDataAsync(request.ConsumerId);

            // Assert
            Assert.Null(actual);

            _mfaProxy.Verify(x =>
                    x.GetMfaSmsDataAsync(
                        It.Is<long>(y => y == request.ConsumerId)),
                Times.Once);
        }

        [Theory()]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task Given_Non404_RefitApiException_GetMfaSmsDataAsync_Should_Throw_MfaApiException(HttpStatusCode statusCode)
        {
            // Arrange
            var request = Fixture.Build<GetMfaSmsDataQuery>()
                .With(x => x.ConsumerId, 1234)
                .Create();

            var refitException = await Refit.ApiException.Create(
                new HttpRequestMessage(),
                HttpMethod.Get,
                new HttpResponseMessage(statusCode)
            );

            _mfaProxy.Setup(x => x.GetMfaSmsDataAsync(It.IsAny<long>()))
                .ThrowsAsync(refitException);

            // Act & Assert
            await Assert.ThrowsAsync<MfaApiException>(async () =>
                await _target.GetMfaSmsDataAsync(request.ConsumerId)
            );
        }

        [Fact]
        public async Task Given_Exception_Caught_GetMfaSmsDataAsync_Should_Throw()
        {
            // Arrange
            var request = Fixture.Build<GetMfaSmsDataQuery>()
                .With(x => x.ConsumerId, 1234)
                .Create();

            _mfaProxy.Setup(x => x.GetMfaSmsDataAsync(It.IsAny<long>()))
                .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
                await _target.GetMfaSmsDataAsync(request.ConsumerId)
            );
        }
    }
}