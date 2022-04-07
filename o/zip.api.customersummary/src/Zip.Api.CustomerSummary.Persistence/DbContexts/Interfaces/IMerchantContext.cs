using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces
{
    public interface IMerchantContext
    {
        Task<Merchant> GetAsync(long id);
    }
}