using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces
{
    public interface IConsumerStatContext
    {
        Task<ConsumerStat> GetByConsumerIdAsync(long consumerId);
        Task SaveAsync(ConsumerStat consumerStat);
        Task<ConsumerStat> GetAsync(long id);
    }
}