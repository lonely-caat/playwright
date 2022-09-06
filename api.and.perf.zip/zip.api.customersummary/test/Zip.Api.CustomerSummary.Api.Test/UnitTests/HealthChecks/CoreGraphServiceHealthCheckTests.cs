using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.HealthChecks;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Response;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.HealthChecks
{
    public class CoreGraphServiceHealthCheckTests
    {
        private readonly Mock<ICoreGraphServiceProxy> _coreGraphServiceProxy = new Mock<ICoreGraphServiceProxy>();

        [Fact]
        public async Task Should_Return_Healthy()
        {
            _coreGraphServiceProxy.Setup(x => x.HealthCheckAsync())
                                  .ReturnsAsync(new CoreGraphServiceHealthCheckResponse { Status = "pass" });

            var target = new CoreGraphServiceHealthCheck(_coreGraphServiceProxy.Object);
            var result = await target.CheckHealthAsync(new HealthCheckContext(), CancellationToken.None);

            Assert.Equal(HealthStatus.Healthy, result.Status);
        }

        [Fact]
        public async Task Should_Return_Unhealthy()
        {
            _coreGraphServiceProxy.Setup(x => x.HealthCheckAsync())
                                  .ReturnsAsync(new CoreGraphServiceHealthCheckResponse { Status = "fail" });

            var target = new CoreGraphServiceHealthCheck(_coreGraphServiceProxy.Object);
            var result = await target.CheckHealthAsync(new HealthCheckContext(), CancellationToken.None);

            Assert.Equal(HealthStatus.Unhealthy, result.Status);
        }
    }
}
