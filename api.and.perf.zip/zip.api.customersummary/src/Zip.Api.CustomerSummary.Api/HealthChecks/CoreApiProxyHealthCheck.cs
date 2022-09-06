using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.HealthChecks
{
    public class CoreApiProxyHealthCheck : IHealthCheck
    {
        private readonly ICoreServiceProxy _coreServiceProxy;

        public CoreApiProxyHealthCheck(ICoreServiceProxy coreServiceProxy)
        {
            _coreServiceProxy = coreServiceProxy;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            var response = await _coreServiceProxy.HealthCheckAsync();

            return new HealthCheckResult(response.IsSuccessStatusCode
                                             ? HealthStatus.Healthy
                                             : HealthStatus.Unhealthy);
        }
    }
}