using System.Threading.Tasks;
using Refit;
using Zip.Api.CustomerSummary.Domain.Entities.Mfa;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.MfaService.Interfaces
{
    public interface IMfaProxy
    {
        [Get("/api/consumers/{consumerId}/sms-data")]
        Task<MfaSmsDataResponse> GetMfaSmsDataAsync(long consumerId);
    }
}