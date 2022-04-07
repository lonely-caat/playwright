using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;
using Zip.Api.CustomerSummary.Infrastructure.Services.MerchantDashboardService;
using Zip.Api.CustomerSummary.Infrastructure.Services.MerchantDashboardService.Interface;
using Zip.Api.CustomerSummary.Infrastructure.Services.MerchantDashboardService.Models.Requests;
using Zip.Api.CustomerSummary.Infrastructure.Services.MerchantDashboardService.Models.Responses;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Services
{
    public class MerchantDashboardServiceTests
    {
        private readonly MerchantDashboardService _target;

        private readonly Mock<IMerchantDashboardApiProxy> _proxy;

        private readonly Fixture _fixture;

        public MerchantDashboardServiceTests()
        {
            _proxy = new Mock<IMerchantDashboardApiProxy>();
            _fixture = new Fixture();
            var logger = new Mock<ILogger<MerchantDashboardService>>();

            _target = new MerchantDashboardService(logger.Object, _proxy.Object);
        }

        [Fact]
        public async Task Given_Successful_Response_When_GetOrderSummaryAsync_Should_Not_Throw_Exception()
        {
            // Arrange
            var request = _fixture.Create<OrderSummaryRequest>();
            var response = _fixture.Create<OrderSummaryResponse>();

            _proxy.Setup(x => x.SendGetOrderSummaryRequestAsync(request, CancellationToken.None))
                  .ReturnsAsync(new HttpResponseMessage
                   {
                       StatusCode = HttpStatusCode.OK,
                       Content = new StringContent(response.ToJsonString())
                   });

            // Act
            Func<Task> func = async () => { await _target.GetOrderSummaryAsync(request, CancellationToken.None); };

            // Assert
            await func.Should().NotThrowAsync<Exception>();
            _proxy.Verify(x => x.SendGetOrderSummaryRequestAsync(request, CancellationToken.None), Times.Once);
        }

        [Theory]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        [InlineData(HttpStatusCode.GatewayTimeout)]
        public async Task Given_Unsuccessful_Response_When_GetOrderSummaryAsync_Should_Throw_Exception(HttpStatusCode statusCode)
        {
            // Arrange
            var request = _fixture.Create<OrderSummaryRequest>();

            _proxy.Setup(x => x.SendGetOrderSummaryRequestAsync(request, CancellationToken.None))
                  .ReturnsAsync(new HttpResponseMessage { StatusCode = statusCode });

            // Act
            Func<Task> func = async () => { await _target.GetOrderSummaryAsync(request, CancellationToken.None); };

            // Assert;
            await func.Should().ThrowAsync<MerchantDashboardApiException>();
        }
    }
}
