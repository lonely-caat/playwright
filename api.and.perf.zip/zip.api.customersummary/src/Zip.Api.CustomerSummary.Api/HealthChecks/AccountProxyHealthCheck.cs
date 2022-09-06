using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.HealthChecks
{
    public class AccountProxyHealthCheck : IHealthCheck
    {
        private readonly IAccountsService _accountsService;

        public AccountProxyHealthCheck(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            var response = await _accountsService.Ping();

            return new HealthCheckResult(response.Equals("healthy", StringComparison.OrdinalIgnoreCase)
                                             ? HealthStatus.Healthy
                                             : HealthStatus.Unhealthy);
        }
    }
}