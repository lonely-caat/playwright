using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Mocks.DbContexts
{
    public class MockCustomerAttributeContext : ICustomerAttributeContext
    {
        public Task<IEnumerable<Attribute>> GetConsumerAttributesAsync(long consumerId)
        {
            if(consumerId % 2 == 0)
            {
                return Task.FromResult<IEnumerable<Attribute>>(new List<Attribute>
                {
                    new Attribute
                    {
                        Active = true,
                        Category = AttributeCategory.GoodCredit,
                        Name = "Good",
                        Type = AttributeType.ConsumerCredit
                    },
                    new Attribute
                    {
                        Active = true,
                        Category = AttributeCategory.CollectionOutcome,
                        Name = "Outcome",
                        Type = AttributeType.ConsumerCollection
                    }
                });
            }

            return Task.FromResult(null as IEnumerable<Attribute>);
        }
    }
}
