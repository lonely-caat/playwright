using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Mfa;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.MfaService.Interfaces
{
    public interface IMfaService
    {
        public Task<MfaSmsDataResponse> GetMfaSmsDataAsync(long consumerId);
    }
}