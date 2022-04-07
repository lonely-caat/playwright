using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Infrastructure.Services.KleberService.Models;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.KleberService.Interfaces
{
    public interface IAddressValidator
    {
        Task<VerifyAddressResponse> ValidateByKelberAsync(
            string unitNumber,
            string streetNumber,
            string streetName,
            string locality,
            string postcode,
            string state);

        bool IsEnabled { get; }
    }
}
