using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Zip.Api.CustomerSummary.Domain.Entities.Communications;
using Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Models;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Mocks.Services
{
    public class MockCommunicationsServiceProxy : ICommunicationsServiceProxy
    {
        public Task<SmsContent> GetSmsContentAsync(string name)
        {
            return Task.FromResult(new SmsContent
            {
                Active = true,
                Content = "Hello testing"
            });
        }

        public Task<string> HealthCheck()
        {
            return Task.FromResult(JsonConvert.SerializeObject(new HealthCheckResult(HealthStatus.Healthy)));
        }

        public Task<CommunicationApiResponse> SendCloseAccountEmailAsync(CloseAccount request)
        {
            return Task.FromResult(new CommunicationApiResponse
            {
                Success = true
            });
        }

        public Task<CommunicationApiResponse> SendPaidOutAndClosedEmailAsync(PaidOutAndClosedEmail request)
        {
            return Task.FromResult(new CommunicationApiResponse
            {
                Success = true
            });
        }

        public Task<SmsResponse> SendPayNowLinkAsync(SendPayNowLink request)
        {
            return Task.FromResult(new SmsResponse
            {
                Success = true
            });
        }

        public Task<CommunicationApiResponse> SendResetPasswordEmailAsync(ResetPassword request)
        {
            return Task.FromResult(new CommunicationApiResponse
            {
                Success = true
            });
        }

        public Task<SmsResponse> SendSmsAsync(SendSms request)
        {
            return Task.FromResult(new SmsResponse
            {
                Success = true
            });
        }
    }
}
