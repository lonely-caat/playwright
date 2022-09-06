using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.HealthChecks;
using Zip.Api.CustomerSummary.Infrastructure.Services.IdentityService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.HealthChecks
{
    public class UserManagementProxyHealthCheckTests
    {
        private readonly Mock<IUserManagementProxy> _target;

        public UserManagementProxyHealthCheckTests()
        {
            _target = new Mock<IUserManagementProxy>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new UserManagementProxyHealthCheck(null);
            });
        }

        [Fact]
        public async Task Should_return_healthy()
        {
            _target.Setup(x => x.HealthCheck())
                   .ReturnsAsync("HeAlThY");

            var target = new UserManagementProxyHealthCheck(_target.Object);
            
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
                var target = new UserManagementProxyHealthCheck(_target.Object);

                await target.CheckHealthAsync(new HealthCheckContext(), CancellationToken.None);
            });
        }
    }
}
