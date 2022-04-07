using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces
{
    public interface ICustomerAttributeContext
    {
        Task<IEnumerable<Attribute>> GetConsumerAttributesAsync(long consumerId);
    }
}