using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.HealthChecks;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountSearchService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.HealthChecks
{
    public class AccountSearchHealthCheckTest
    {
        private readonly Mock<IAccountSearchServiceClient> _accountSearchServiceClient;

        public AccountSearchHealthCheckTest()
        {
            _accountSearchServiceClient = new Mock<IAccountSearchServiceClient>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new AccountSearchHealthCheck(null);
            });
        }

        [Fact]
        public async Task Should_return_healthy()
        {
            _accountSearchServiceClient.Setup(x => x.GetStatusAsync());

            var hc = new AccountSearchHealthCheck(_accountSearchServiceClient.Object);
            var r = await hc.CheckHealthAsync(new Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckContext(), CancellationToken.None);

            Assert.Equal(HealthStatus.Healthy, r.Status);
        }

        [Fact]
        public async Task Should_return_unhealthy()
        {
            _accountSearchServiceClient.Setup(x => x.GetStatusAsync())
                .ThrowsAsync(new Exception());

            await Assert.ThrowsAsync<Exception>(async () =>
            {
                var hc = new AccountSearchHealthCheck(_accountSearchServiceClient.Object);
                await hc.CheckHealthAsync(new Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckContext(), CancellationToken.None);
            });
        }
    }
}
