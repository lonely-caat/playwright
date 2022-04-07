using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Zip.Api.CustomerSummary.Infrastructure.Services.StatementService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.HealthChecks
{
    public class StatementsApiProxyHealthCheck : IHealthCheck
    {
        private readonly IStatementsApiProxy _statementsApiProxy;

        public StatementsApiProxyHealthCheck(IStatementsApiProxy statementsApiProxy)
        {
            _statementsApiProxy = statementsApiProxy;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            var response = await _statementsApiProxy.HealthCheck();

            return new HealthCheckResult(
                string.Equals(response, "Healthy", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(response, "OK", StringComparison.OrdinalIgnoreCase)
                    ? HealthStatus.Healthy
                    : HealthStatus.Unhealthy);
        }
    }
}
