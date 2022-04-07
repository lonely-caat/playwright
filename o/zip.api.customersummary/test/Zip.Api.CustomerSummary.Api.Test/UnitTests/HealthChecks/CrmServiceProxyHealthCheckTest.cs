using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.HealthChecks;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Crm;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.HealthChecks
{
    public class CrmServiceProxyHealthCheckTest
    {
        private readonly Mock<ICrmServiceProxy> _target;

        public CrmServiceProxyHealthCheckTest()
        {
            _target = new Mock<ICrmServiceProxy>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new CrmServiceProxyHealthCheck(null);
            });
        }

        [Fact]
        public async Task Should_return_healthy()
        {
            _target.Setup(x => x.HealthCheck())
                   .ReturnsAsync("HeAlThY");

            var target = new CrmServiceProxyHealthCheck(_target.Object);

            var result = await target.CheckHealthAsync(new HealthCheckContext(), CancellationToken.None);

            Assert.Equal(HealthStatus.Healthy, result.Status);
        }

        [Fact]
        public async Task Should_return_unhealthy()
        {
            _target.Setup(x => x.HealthCheck())
                   .ThrowsAsync(new Exception());

            await Assert.ThrowsAsync<Exception>(async () =>
            {
                var target = new CrmServiceProxyHealthCheck(_target.Object);

                await target.CheckHealthAsync(new HealthCheckContext(), CancellationToken.None);
            });
        }
    }
}
