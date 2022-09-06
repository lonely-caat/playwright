using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.HealthChecks;
using Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.HealthChecks
{
    public class CommunicationsServiceProxyHealthCheckTest
    {
        private readonly Mock<ICommunicationsServiceProxy> _target;

        public CommunicationsServiceProxyHealthCheckTest()
        {
            _target = new Mock<ICommunicationsServiceProxy>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new CommunicationsServiceProxyHealthCheck(null);
            });
        }

        [Fact]
        public async Task Should_return_healthy()
        {
            _target.Setup(x => x.HealthCheck())
                   .ReturnsAsync("HeAlThY");

            var target = new CommunicationsServiceProxyHealthCheck(_target.Object);

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
                var target = new CommunicationsServiceProxyHealthCheck(_target.Object);

                await target.CheckHealthAsync(new HealthCheckContext(), CancellationToken.None);
            });
        }
    }
}
