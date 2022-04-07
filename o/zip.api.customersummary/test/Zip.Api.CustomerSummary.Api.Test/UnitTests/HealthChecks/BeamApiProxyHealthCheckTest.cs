using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.HealthChecks;
using Zip.Api.CustomerSummary.Infrastructure.Services.BeamService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.HealthChecks
{
    public class BeamApiProxyHealthCheckTest
    {
        private readonly Mock<IBeamProxy> _beamProxy = new Mock<IBeamProxy>();
        
        [Fact]
        public async Task Should_Return_Healthy()
        {
            _beamProxy.Setup(x => x.Ping())
                      .ReturnsAsync("pong");

            var target = new BeamApiProxyHealthCheck(_beamProxy.Object);
            var result = await target.CheckHealthAsync(new HealthCheckContext(), CancellationToken.None);

            Assert.Equal(HealthStatus.Healthy, result.Status);
        }
        
        [Fact]
        public async Task Should_Return_Unhealthy()
        {
            _beamProxy.Setup(x => x.Ping())
                      .ReturnsAsync("unhealthy");

            var target = new BeamApiProxyHealthCheck(_beamProxy.Object);
            var result = await target.CheckHealthAsync(new HealthCheckContext(), CancellationToken.None);

            Assert.Equal(HealthStatus.Unhealthy, result.Status);
        }
    }
}
