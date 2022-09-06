using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces
{
    public interface IAttributeContext
    {
        Task<IEnumerable<Attribute>> GetAttributesAsync();

        Task<IEnumerable<ConsumerAttributeDto>> GetConsumerAttributesAsync(long consumerId);

        Task SetConsumerAttributesAsync(long consumerId, List<long> attributes);
    }
}