using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.HealthChecks;
using Zip.Api.CustomerSummary.Infrastructure.Services.StatementService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.HealthChecks
{
    public class StatementsApiProxyHealthCheckTests
    {
        private readonly Mock<IStatementsApiProxy> _statementsApiProxy;
        private readonly StatementsApiProxyHealthCheck _target;

        public StatementsApiProxyHealthCheckTests()
        {
            _statementsApiProxy = new Mock<IStatementsApiProxy>();
            _target = new StatementsApiProxyHealthCheck(_statementsApiProxy.Object);
        }

        [Fact]
        public async Task Should_return_healthy()
        {
            // Arrange
            _statementsApiProxy.Setup(x => x.HealthCheck())
                               .ReturnsAsync("OK");

            // Act
            var result = await _target.CheckHealthAsync(new HealthCheckContext(), CancellationToken.None);

            // Assert
            result.Status.Should().Be(HealthStatus.Healthy);
        }

        [Fact]
        public async Task Should_return_unhealthy()
        {
            // Arrange
            _statementsApiProxy.Setup(x => x.HealthCheck())
                               .ThrowsAsync(new Exception());
            
            // Act
            Func<Task> func = async () =>
            {
                await _target.CheckHealthAsync(new HealthCheckContext(), CancellationToken.None);
            };

            // Action
            await func.Should().ThrowAsync<Exception>();
        }
    }
}
