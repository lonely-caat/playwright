using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Zip.Api.CustomerSummary.Infrastructure.Services.MerchantDashboardService.Interface;

namespace Zip.Api.CustomerSummary.Api.HealthChecks
{
    public class MerchantDashboardApiProxyHealthCheck : IHealthCheck
    {
        private readonly IMerchantDashboardApiProxy _proxy;

        public MerchantDashboardApiProxyHealthCheck(IMerchantDashboardApiProxy proxy)
        {
            _proxy = proxy;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var response = await _proxy.HealthCheck();

            return new HealthCheckResult(!string.IsNullOrEmpty(response) && response.ToUpperInvariant() == "HEALTHY"
                                             ? HealthStatus.Healthy
                                             : HealthStatus.Unhealthy);
        }
    }
}