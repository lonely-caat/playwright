using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreService.Models;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.CoreService.Interfaces
{
    public interface ICoreService
    {
        Task<GetCoreTokenResponse> GetCoreTokenAsync(CancellationToken cancellationToken);

        Task<HttpResponseMessage> SendResetPasswordEmailAsync(CoreResetPasswordModel coreResetPassword, string token, CancellationToken cancellationToken);
    }
}