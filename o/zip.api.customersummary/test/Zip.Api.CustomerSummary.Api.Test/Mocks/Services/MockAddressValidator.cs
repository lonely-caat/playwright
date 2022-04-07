using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Infrastructure.Services.KleberService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.KleberService.Models;

namespace Zip.Api.CustomerSummary.Api.Test.Mocks.Services
{
    public class MockAddressValidator : IAddressValidator
    {
        public bool IsEnabled => true;

        public Task<VerifyAddressResponse> ValidateByKelberAsync(string unitNumber, string streetNumber, string streetName, string locality, string postcode, string state)
        {
            return Task.FromResult(new VerifyAddressResponse
            {
                DtResponse = new VerifyAddressInnerResponse()
            });
        }
    }
}
