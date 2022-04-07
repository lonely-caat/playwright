using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Customers;

namespace Zip.Api.CustomerSummary.Api.HealthChecks
{
    public class CustomersServiceHealthCheck : IHealthCheck
    {
        private readonly ICustomersServiceProxy _customersServiceProxy;

        public CustomersServiceHealthCheck(ICustomersServiceProxy customersServiceProxy)
        {
            _customersServiceProxy = customersServiceProxy ?? throw new ArgumentNullException(nameof(customersServiceProxy));
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var response = await _customersServiceProxy.Ping();
            return JsonConvert.DeserializeObject<HealthCheckResult>(response);
        }
    }
}
