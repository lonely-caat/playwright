using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreService.Models;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Mocks.Services
{
    public class MockCoreService : ICoreService
    {
        public async Task<GetCoreTokenResponse> GetCoreTokenAsync(CancellationToken cancellationToken)
        {
            return new GetCoreTokenResponse
            {
                AccessToken = "123456789",
                ExpiresIn = 300,
                TokenType = "Bearer"
            };
        }

        public async Task<HttpResponseMessage> SendResetPasswordEmailAsync(CoreResetPasswordModel coreResetPassword, string token, CancellationToken cancellationToken)
        {
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
    }
}
