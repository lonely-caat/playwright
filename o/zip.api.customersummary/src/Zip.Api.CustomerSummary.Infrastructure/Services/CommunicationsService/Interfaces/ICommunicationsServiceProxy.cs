using System.Threading.Tasks;
using Refit;
using Zip.Api.CustomerSummary.Domain.Entities.Communications;
using Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Models;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Interfaces
{
    public interface ICommunicationsServiceProxy
    {
        [Get("/health")]
        Task<string> HealthCheck();

        [Post("/api/emails/send/close-account")]
        Task<CommunicationApiResponse> SendCloseAccountEmailAsync(CloseAccount request);

        [Post("/api/emails/send/reset-password")]
        Task<CommunicationApiResponse> SendResetPasswordEmailAsync(ResetPassword request);

        [Get("/api/sms/content/{name}")]
        Task<SmsContent> GetSmsContentAsync(string name);

        [Post("/api/sms/send")]
        Task<SmsResponse> SendSmsAsync(SendSms request);

        [Post("/api/sms/send/paynowlink")]
        Task<SmsResponse> SendPayNowLinkAsync(SendPayNowLink request);

        [Post("/api/emails/send/account-paidout")]
        Task<CommunicationApiResponse> SendPaidOutAndClosedEmailAsync(PaidOutAndClosedEmail request);
    }
}
