using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.HealthChecks;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Customers;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.HealthChecks
{
    public class CustomersServiceHealthCheckTest
    {
        private readonly Mock<ICustomersServiceProxy> customersServiceProxy = new Mock<ICustomersServiceProxy>();

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new CustomersServiceHealthCheck(null);
            });
        }

        [Fact]
        public async Task Given_ProxyReturnsHealthCheckResponse_ShouldReturn_It()
        {
            var expectedHealthCheckResult = new HealthCheckResult(HealthStatus.Unhealthy, description:"test description");

            customersServiceProxy.Setup(x => x.Ping())
                .ReturnsAsync(JsonConvert.SerializeObject(expectedHealthCheckResult));

            var hc = new CustomersServiceHealthCheck(customersServiceProxy.Object);
            var r = await hc.CheckHealthAsync(new HealthCheckContext(), default);

            Assert.Equal(expectedHealthCheckResult.Status, r.Status);
            Assert.Equal("test description", r.Description);
        }
    }
}
