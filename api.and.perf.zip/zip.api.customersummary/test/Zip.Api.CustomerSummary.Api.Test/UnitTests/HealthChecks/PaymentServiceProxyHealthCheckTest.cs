using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.HealthChecks;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Payments;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.HealthChecks
{
    public class PaymentServiceProxyHealthCheckTest
    {
        private readonly Mock<IPaymentsServiceProxy> _paymentsServiceProxy;

        public PaymentServiceProxyHealthCheckTest()
        {
            _paymentsServiceProxy = new Mock<IPaymentsServiceProxy>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new PaymentServiceProxyHealthCheck(null));
        }

        [Fact]
        public async Task Should_return_healthy()
        {
            _paymentsServiceProxy.Setup(x => x.Ping())
                .ReturnsAsync("\"pong\"");

            var hc = new PaymentServiceProxyHealthCheck(_paymentsServiceProxy.Object);
            var r = await hc.CheckHealthAsync(new Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckContext(), CancellationToken.None);

            Assert.Equal(HealthStatus.Healthy, r.Status);
        }

        [Fact]
        public async Task Should_return_unhealthy()
        {
            _paymentsServiceProxy.Setup(x => x.Ping())
                .ReturnsAsync("\"pongxxx1\"");

            var hc = new PaymentServiceProxyHealthCheck(_paymentsServiceProxy.Object);
            var r = await hc.CheckHealthAsync(new Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckContext(), CancellationToken.None);

            Assert.Equal(HealthStatus.Unhealthy, r.Status);
        }
    }
}
