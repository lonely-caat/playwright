using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.GoogleAddress;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.GoogleService.Interfaces
{
    public interface IAddressSearch
    {
        Task<GoogleAddress> FindAndVerifyAsync(string addressLine);

        Task<IEnumerable<Prediction>> SearchAsync(string countryCode, string keyword);
    }
}
