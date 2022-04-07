using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.HealthChecks
{
    public class CoreGraphServiceHealthCheck : IHealthCheck
    {
        private readonly ICoreGraphServiceProxy _coreGraphServiceProxy;

        public CoreGraphServiceHealthCheck(ICoreGraphServiceProxy coreGraphServiceProxy)
        {
            _coreGraphServiceProxy = coreGraphServiceProxy;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            var response = await _coreGraphServiceProxy.HealthCheckAsync();

            return new HealthCheckResult(response.Status.Equals("pass", StringComparison.OrdinalIgnoreCase)
                                             ? HealthStatus.Healthy
                                             : HealthStatus.Unhealthy);
        }
    }
}
