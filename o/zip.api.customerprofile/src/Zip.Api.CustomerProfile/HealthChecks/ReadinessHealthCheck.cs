using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Zip.Api.CustomerProfile.Interfaces;

namespace Zip.Api.CustomerProfile.HealthChecks
{
    public class ReadinessHealthCheck : IHealthCheck
    {
        private readonly IServiceWarmupState _state;

        public ReadinessHealthCheck(IServiceWarmupState state)
        {
            _state = state;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_state.IsReady ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy());
        }
    }
}