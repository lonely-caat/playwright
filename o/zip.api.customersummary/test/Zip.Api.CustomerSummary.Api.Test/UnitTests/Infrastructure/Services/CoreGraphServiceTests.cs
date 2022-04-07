using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Request.GetUpcomingInstallments;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Request.PayOrder;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Response;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Response.PayOrder;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Services
{
    public class CoreGraphServiceTests : CommonTestsFixture
    {
        private readonly CoreGraphService _target;

        private readonly Mock<ICoreGraphServiceProxy> _coreGraphServiceProxy;

        public CoreGraphServiceTests()
        {
            var logger = new Mock<ILogger<CoreGraphService>>();
            _coreGraphServiceProxy = new Mock<ICoreGraphServiceProxy>();

            _target = new CoreGraphService(logger.Object, _coreGraphServiceProxy.Object);
        }

        [Fact]
        public async Task Given_Success_On_CoreGraphProxy_GetUpcomingInstallmentsAsync_Should_Return()
        {
            // Arrange
            var installments = Fixture.CreateMany<Installment>();
            var account = Fixture.Build<GetUpcomingInstallmentsAccount>()
                                 .With(x => x.UpcomingInstallments, installments)
                                 .Create();
            var data = Fixture.Build<GetUpcomingInstallmentsData>()
                              .With(x => x.AccountV1, account)
                              .Create();
            var response = Fixture.Build<GetUpcomingInstallmentsResponse>()
                                  .With(x => x.Data, data)
                                  .Create();

            _coreGraphServiceProxy
               .Setup(x => x.GetUpcomingInstallmentsAsync(It.IsAny<GetUpcomingInstallmentsRequest>()))
               .ReturnsAsync(response);

            // Act
            var result = await _target.GetUpcomingInstallmentsAsync(123, Guid.NewGuid(), DateTime.Now);

            // Assert
            Assert.Equal(installments.ToJsonString(), result.ToJsonString());
            _coreGraphServiceProxy.Verify(x =>
                                              x.GetUpcomingInstallmentsAsync(
                                                  It.IsAny<GetUpcomingInstallmentsRequest>()),
                                          Times.Once());
        }

        [Fact]
        public async Task Given_NotFound_RefitApiException_On_GetUpcomingInstallmentsAsync_Should_Return_Null()
        {
            // Arrange
            var refitNotFoundException = await Refit.ApiException.Create(
                                             new HttpRequestMessage(),
                                             HttpMethod.Get,
                                             new HttpResponseMessage(HttpStatusCode.NotFound)
                                         );
            _coreGraphServiceProxy
               .Setup(x => x.GetUpcomingInstallmentsAsync(It.IsAny<GetUpcomingInstallmentsRequest>()))
               .ThrowsAsync(refitNotFoundException);

            // Act
            var result = await _target.GetUpcomingInstallmentsAsync(123, Guid.NewGuid(), DateTime.Now);

            // Assert
            Assert.Null(result);
            _coreGraphServiceProxy.Verify(x =>
                                              x.GetUpcomingInstallmentsAsync(
                                                  It.IsAny<GetUpcomingInstallmentsRequest>()),
                                          Times.Once);
        }

        [Fact]
        public async Task Given_NonNotFound_RefitApiException_On_GetUpcomingInstallmentsAsync_Should_Throw_CoreGraphException()
        {
            // Arrange
            var refitException = await Refit.ApiException.Create(
                                     new HttpRequestMessage(),
                                     HttpMethod.Get,
                                     new HttpResponseMessage(HttpStatusCode.InternalServerError)
                                 );
            _coreGraphServiceProxy
               .Setup(x => x.GetUpcomingInstallmentsAsync(It.IsAny<GetUpcomingInstallmentsRequest>()))
               .ThrowsAsync(refitException);

            // Act & Assert
            await Assert.ThrowsAsync<CoreGraphException>(async () =>
                                                             await _target.GetUpcomingInstallmentsAsync(
                                                                 123,
                                                                 Guid.NewGuid(),
                                                                 DateTime.Now)
            );
        }

        [Fact]
        public async Task Given_Exception_On_GetUpcomingInstallmentsAsync_Should_Throw()
        {
            // Arrange
            _coreGraphServiceProxy.Setup(x => x.GetUpcomingInstallmentsAsync(
                                             It.IsAny<GetUpcomingInstallmentsRequest>()))
                                  .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
                                                    await _target.GetUpcomingInstallmentsAsync(
                                                        123,
                                                        Guid.NewGuid(),
                                                        DateTime.Now)
            );
        }

        [Fact]
        public async Task Given_Success_On_CoreGraphProxy_PayOrderAsync_Should_Return()
        {
            // Arrange
            var innerResponse = Fixture.Create<PayOrderInnerResponse>();
            var data = Fixture.Build<PayOrderData>()
                              .With(x => x.InnerResponse, innerResponse)
                              .Create();
            var response = Fixture.Build<PayOrderResponse>()
                                  .With(x => x.Data, data)
                                  .Create();

            _coreGraphServiceProxy
               .Setup(x => x.PayOrderAsync(It.IsAny<string>(), It.IsAny<PayOrderRequest>()))
               .ReturnsAsync(response);

            // Act
            var result = await _target.PayOrderAsync(Fixture.Create<PayOrderInput>());

            // Assert
            Assert.Equal(innerResponse.ToJsonString(), result.ToJsonString());
            _coreGraphServiceProxy.Verify(x =>
                                              x.PayOrderAsync(
                                                  It.IsAny<string>(),
                                                  It.IsAny<PayOrderRequest>()),
                                          Times.Once());
        }

        [Fact]
        public async Task Given_RefitApiException_On_PayOrderAsync_Should_Throw_CoreGraphException()
        {
            // Arrange
            var refitException = await Refit.ApiException.Create(
                                     new HttpRequestMessage(),
                                     HttpMethod.Get,
                                     new HttpResponseMessage(HttpStatusCode.InternalServerError)
                                 );
            _coreGraphServiceProxy
               .Setup(x => x.PayOrderAsync(It.IsAny<string>(), It.IsAny<PayOrderRequest>()))
               .ThrowsAsync(refitException);

            // Act & Assert
            await Assert.ThrowsAsync<CoreGraphException>(async () =>
                                                             await _target.PayOrderAsync(
                                                                 Fixture.Create<PayOrderInput>())
            );
        }

        [Fact]
        public async Task Given_Exception_On_PayOrderAsync_Should_Throw()
        {
            // Arrange
            _coreGraphServiceProxy.Setup(x => x.PayOrderAsync(It.IsAny<string>(), It.IsAny<PayOrderRequest>()))
                                  .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
                                                    await _target.PayOrderAsync(
                                                        Fixture.Create<PayOrderInput>())
            );
        }
    }
}