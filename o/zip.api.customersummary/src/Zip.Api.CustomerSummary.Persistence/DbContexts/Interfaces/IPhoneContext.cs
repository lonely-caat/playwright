using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces
{
    public interface IPhoneContext
    {
        Task<Phone> GetAsync(long id);
        Task<Phone> UpdateStatusAsync(long consumerId, Phone phone);
        Task<IEnumerable<Phone>> GetConsumerPhoneHistoryAsync(long consumerId);
        Task<ConsumerPhone> GetConsumerPhoneAsync(long consumerId, long phoneId);
    }
}