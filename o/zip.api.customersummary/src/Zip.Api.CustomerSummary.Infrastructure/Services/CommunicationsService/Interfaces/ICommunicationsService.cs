using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Models;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Interfaces
{
    public interface ICommunicationsService
    {
         Task<CommunicationApiResponse> SendPaidOutCloseEmailAsync(PaidOutAndClosedEmail request);

         Task<CommunicationApiResponse> SendResetPasswordAsync(ResetPassword request);

         Task<CommunicationApiResponse> SendCloseAccountAsync(CloseAccount request);
    }
}