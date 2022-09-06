using Microsoft.Extensions.Diagnostics.HealthChecks;
using Refit;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Infrastructure.Services.CustomerCoreService.Models;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.CustomerCoreService.Interfaces
{
    public interface ICustomerCoreServiceProxy
    {
        [Get("/healthz")]
        Task<HealthCheckResult> HealthCheckAsync();
        
        [Get("/api/v1/Customer/api/login/status/{email}")]
        Task<HttpResponseMessage> GetLoginStatusAsync(
            string email,
            CancellationToken cancellationToken);

        [Post("/api/v1/Customer/api/login/disable")]
        Task<HttpResponseMessage> PostDisableLoginAsync(
            UpdateLoginStatusRequest request,
            CancellationToken cancellationToken);

        [Post("/api/v1/Customer/api/login/enable")]
        Task<HttpResponseMessage> PostEnableLoginAsync(
            UpdateLoginStatusRequest request,
            CancellationToken cancellationToken);
    }
}