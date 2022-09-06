using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.HealthChecks;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.HealthChecks
{
    public class CoreApiProxyHealthCheckTest
    {
        private readonly Mock<ICoreServiceProxy> _target;

        public CoreApiProxyHealthCheckTest()
        {
            _target = new Mock<ICoreServiceProxy>();
        }

        [Fact]
        public async Task Should_return_healthy()
        {
            _target.Setup(x => x.HealthCheckAsync())
                   .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.Accepted));

            var target = new CoreApiProxyHealthCheck(_target.Object);

            var result = await target.CheckHealthAsync(new HealthCheckContext(), CancellationToken.None);

            Assert.Equal(HealthStatus.Healthy, result.Status);
        }

        [Fact]
        public async Task Should_return_unhealthy()
        {
            _target.Setup(x => x.HealthCheckAsync())
                   .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest));
            
            var target = new CoreApiProxyHealthCheck(_target.Object);

            var result = await target.CheckHealthAsync(new HealthCheckContext(), CancellationToken.None);

            Assert.Equal(HealthStatus.Unhealthy, result.Status);
        }
    }
}
