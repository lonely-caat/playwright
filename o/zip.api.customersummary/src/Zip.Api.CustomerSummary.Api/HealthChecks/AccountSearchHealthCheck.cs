using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountSearchService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.HealthChecks
{
    public class AccountSearchHealthCheck : IHealthCheck
    {
        private readonly IAccountSearchServiceClient _client;

        public AccountSearchHealthCheck(IAccountSearchServiceClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            await _client.GetStatusAsync();

            return new HealthCheckResult(HealthStatus.Healthy);
        }
    }
}
