using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.HealthChecks
{
    public class CommunicationsServiceProxyHealthCheck : IHealthCheck
    {
        private readonly ICommunicationsServiceProxy _communicationsServiceProxy;

        public CommunicationsServiceProxyHealthCheck(ICommunicationsServiceProxy communicationsServiceProxy)
        {
            _communicationsServiceProxy = communicationsServiceProxy ??
                throw new ArgumentNullException(nameof(communicationsServiceProxy));
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var response = await _communicationsServiceProxy.HealthCheck();

            return new HealthCheckResult(!string.IsNullOrEmpty(response) && response.ToUpperInvariant() == "HEALTHY"
                                             ? HealthStatus.Healthy
                                             : HealthStatus.Unhealthy);
        }
    }
}
