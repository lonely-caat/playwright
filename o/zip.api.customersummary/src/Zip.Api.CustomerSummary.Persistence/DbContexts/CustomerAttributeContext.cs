using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Api.CustomerSummary.Persistence.Interfaces;
using Attribute = Zip.Api.CustomerSummary.Domain.Entities.Consumers.Attribute;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts
{
    public class CustomerAttributeContext : ICustomerAttributeContext
    {
        private readonly IDbContext _dbContext;

        public CustomerAttributeContext(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<Attribute>> GetConsumerAttributesAsync(long consumerId)
        {
            var query = @"
SELECT * 
  FROM ATTRIBUTE 
 WHERE ID IN    (
                    SELECT AttributeId 
                      FROM ConsumerAttribute 
                     WHERE Active = 1 
                       AND ConsumerId=@consumerId
                ) 
   AND Active = 1
";

            return await _dbContext.QueryAsync<Attribute>(query, new { consumerId });
        }
    }
}
