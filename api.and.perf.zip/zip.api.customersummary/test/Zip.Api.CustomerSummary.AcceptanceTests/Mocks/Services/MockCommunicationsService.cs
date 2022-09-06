using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Models;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Mocks.Services
{
    public class MockCommunicationsService : ICommunicationsService
    {
        public Task<CommunicationApiResponse> SendCloseAccountAsync(CloseAccount request)
        {
            return Task.FromResult(new CommunicationApiResponse
            {
                Success = true
            });
        }

        public Task<CommunicationApiResponse> SendPaidOutCloseEmailAsync(PaidOutAndClosedEmail request)
        {
            return Task.FromResult(new CommunicationApiResponse
            {
                Success = true
            });
        }

        public Task<CommunicationApiResponse> SendResetPasswordAsync(ResetPassword request)
        {
            return Task.FromResult(new CommunicationApiResponse
            {
                Success = true
            });
        }
    }
}
