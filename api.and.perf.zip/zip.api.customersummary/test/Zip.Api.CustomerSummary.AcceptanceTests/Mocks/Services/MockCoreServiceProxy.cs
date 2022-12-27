using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Refit;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreService.Models;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Mocks.Services
{
    public class MockCoreServiceProxy : ICoreServiceProxy
    {
        public async Task<GetCoreTokenResponse> GetTokenAsync(
            [Body(BodySerializationMethod.UrlEncoded)]
            Dictionary<string, string> data,
            CancellationToken cancellationToken)
        {
            return new GetCoreTokenResponse
            {
                AccessToken = "123456789",
                ExpiresIn = 300,
                TokenType = "Bearer"
            };
        }

        public async Task<HttpResponseMessage> HealthCheckAsync()
        {
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }

        public async Task<HttpResponseMessage> SendResetPasswordEmailAsync(
            CoreResetPasswordModel coreResetPasswordModel,
            [Sidebar("Authorization")] string token,
            CancellationToken cancellationToken)
        {
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
    }
}