using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Refit;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreService.Models;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.CoreService.Interfaces
{
    public interface ICoreServiceProxy
    {
        [Post("/login/connect/token")]
        Task<GetCoreTokenResponse> GetTokenAsync(
            [Body(BodySerializationMethod.UrlEncoded)]
            Dictionary<string, string> data,
            CancellationToken cancellationToken);

        [Get("/ping")]
        Task<HttpResponseMessage> HealthCheckAsync();

        [Post("/login/api/admin/reset-user-password")]
        Task<HttpResponseMessage> SendResetPasswordEmailAsync(
            CoreResetPasswordModel coreResetPasswordModel,
            [Sidebar("Authorization")] string token,
            CancellationToken cancellationToken);
    }
}