using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces
{
    public interface IAddressContext
    {
        Task<Address> GetAsync(long consumerId);

        Task UpdateAsync(long consumerId, Address address);
    }
}