using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.HealthChecks;
using Zip.Api.CustomerSummary.Infrastructure.Services.CustomerCoreService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.HealthChecks
{
    public class CustomerCoreApiProxyHealthCheckTests
    {
        private readonly Mock<ICustomerCoreServiceProxy> customerCoreServiceProxy = new Mock<ICustomerCoreServiceProxy>();

        [Fact]
        public async Task Given_ProxyReturnsHealthCheckResponse_ShouldReturn_It()
        {
            var expectedHealthCheckResult = new HealthCheckResult(HealthStatus.Unhealthy, description: "test description");

            customerCoreServiceProxy.Setup(x => x.HealthCheckAsync())
                                    .ReturnsAsync(expectedHealthCheckResult);

            var hc = new CustomerCoreApiProxyHealthCheck(customerCoreServiceProxy.Object);
            var r = await hc.CheckHealthAsync(new HealthCheckContext(), default);

            Assert.Equal(expectedHealthCheckResult, r);
        }

        [Fact]
        public async Task Given_ProxyReturnsException_ShouldReturn_Unhealthy()
        {
            customerCoreServiceProxy.Setup(x => x.HealthCheckAsync())
                                    .ThrowsAsync(new Exception());

            var hc = new CustomerCoreApiProxyHealthCheck(customerCoreServiceProxy.Object);
            var r = await hc.CheckHealthAsync(new HealthCheckContext(), default);

            Assert.Equal(HealthStatus.Unhealthy, r.Status);
        }
    }
}