using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Crm;

namespace Zip.Api.CustomerSummary.Api.HealthChecks
{
    public class CrmServiceProxyHealthCheck : IHealthCheck
    {
        private readonly ICrmServiceProxy _crmServiceProxy;

        public CrmServiceProxyHealthCheck(ICrmServiceProxy crmServiceProxy)
        {
            _crmServiceProxy = crmServiceProxy ??
                throw new ArgumentNullException(nameof(crmServiceProxy));
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var response = await _crmServiceProxy.HealthCheck();

            return new HealthCheckResult(!string.IsNullOrEmpty(response) && response.ToUpperInvariant() == "HEALTHY"
                                             ? HealthStatus.Healthy
                                             : HealthStatus.Unhealthy);
        }
    }
}
