using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Zip.Api.CustomerSummary.Infrastructure.Services.IdentityService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.HealthChecks
{
    public class UserManagementProxyHealthCheck : IHealthCheck
    {
        private readonly IUserManagementProxy _userManagementProxy;

        public UserManagementProxyHealthCheck(IUserManagementProxy userManagementProxy)
        {
            _userManagementProxy = userManagementProxy ?? throw new ArgumentNullException(nameof(userManagementProxy));
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            var response = await _userManagementProxy.HealthCheck();
          
            return new HealthCheckResult(!string.IsNullOrEmpty(response) && response.ToUpperInvariant() == "HEALTHY"
                                             ? HealthStatus.Healthy
                                             : HealthStatus.Unhealthy);
        }
    }
}
