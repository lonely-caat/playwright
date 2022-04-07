using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Payments;

namespace Zip.Api.CustomerSummary.Api.HealthChecks
{
    public class PaymentServiceProxyHealthCheck : IHealthCheck
    {
        private readonly IPaymentsServiceProxy _paymentsServiceProxy;

        public PaymentServiceProxyHealthCheck(IPaymentsServiceProxy paymentsServiceProxy)
        {
            _paymentsServiceProxy = paymentsServiceProxy ?? throw new ArgumentNullException(nameof(paymentsServiceProxy));
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var response = await _paymentsServiceProxy.Ping();
            
            return new HealthCheckResult(response.Equals("\"pong\"", StringComparison.OrdinalIgnoreCase)
                                             ? HealthStatus.Healthy
                                             : HealthStatus.Unhealthy);
        }
    }
}
