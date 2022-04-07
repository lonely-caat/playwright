using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Zip.Api.CustomerSummary.Infrastructure.Services.BeamService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.HealthChecks
{
    public class BeamApiProxyHealthCheck : IHealthCheck
    {
        private readonly IBeamProxy _beamProxy;

        public BeamApiProxyHealthCheck(IBeamProxy beamProxy)
        {
            _beamProxy = beamProxy;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            var response = await _beamProxy.Ping();

            return new HealthCheckResult(response.Equals("pong", StringComparison.OrdinalIgnoreCase)
                                             ? HealthStatus.Healthy
                                             : HealthStatus.Unhealthy);
        }
    }
}
