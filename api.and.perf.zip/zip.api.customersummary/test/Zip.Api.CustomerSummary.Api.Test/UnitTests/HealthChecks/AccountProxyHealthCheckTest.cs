using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.HealthChecks;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.HealthChecks
{
    public class AccountProxyHealthCheckTest
    {
        private readonly Mock<IAccountsService> _accountsService = new Mock<IAccountsService>();

        [Fact]
        public async Task Given_ResponseUnhealthy_ShouldReturn_Unhealthy()
        {
            _accountsService.Setup(x => x.Ping())
                .ReturnsAsync("unhealthy");

            var hc = new AccountProxyHealthCheck(_accountsService.Object);
            var hcr = await hc.CheckHealthAsync(new HealthCheckContext(), default);

            Assert.Equal(new HealthCheckResult(HealthStatus.Unhealthy).Status, hcr.Status);
        }

        [Fact]
        public async Task Given_ResponseHealthy_ShouldReturn_Healthy()
        {
            _accountsService.Setup(x => x.Ping())
                .ReturnsAsync("healthy");

            var hc = new AccountProxyHealthCheck(_accountsService.Object);
            var hcr = await hc.CheckHealthAsync(new HealthCheckContext(), default);

            Assert.Equal(new HealthCheckResult(HealthStatus.Healthy).Status, hcr.Status);
        }
    }
}
