using System.Threading.Tasks;
using Refit;
using Zip.Api.CustomerSummary.Infrastructure.Services.KleberService.Models;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.KleberService.Interfaces
{
    public interface IAddressVerificationProxy
    {
        [Get("/KleberWebService/DtKleberService.svc/ProcessQueryStringRequest?OutputFormat=json&Method=DataTools.Verify.Address.AuPaf.VerifyAddress")]
        Task<VerifyAddressResponse> VerifyAddress(
            [AliasAs("RequestKey")] string requestKey,
            VerifyAddressRequest request);
    }
}
