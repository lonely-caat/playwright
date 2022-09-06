using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Zip.Api.CustomerSummary.Infrastructure.Services.CustomerCoreService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.HealthChecks
{
    public class CustomerCoreApiProxyHealthCheck : IHealthCheck
    {
        private readonly ICustomerCoreServiceProxy _customerCoreServiceProxy;
        
        public CustomerCoreApiProxyHealthCheck(ICustomerCoreServiceProxy customerCoreServiceProxy)
        {
            _customerCoreServiceProxy = customerCoreServiceProxy;
        }
        
        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await _customerCoreServiceProxy.HealthCheckAsync();
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Dependency Failure on Customer Core Service", ex);
            }
        }
    }
}