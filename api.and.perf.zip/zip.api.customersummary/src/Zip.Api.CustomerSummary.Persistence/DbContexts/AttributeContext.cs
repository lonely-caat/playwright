using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Api.CustomerSummary.Persistence.Interfaces;
using Attribute = Zip.Api.CustomerSummary.Domain.Entities.Consumers.Attribute;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts
{
    public class AttributeContext : IAttributeContext
    {
        private readonly IDbContext _dbContext;

        public AttributeContext(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<Attribute>> GetAttributesAsync()
        {
            const string sql = @"
SELECT *  FROM ATTRIBUTE WHERE ACTIVE = 1 AND TYPE = 1";
            
            return await _dbContext.QueryAsync<Attribute>(sql);
        }
        
        public async Task<IEnumerable<ConsumerAttributeDto>> GetConsumerAttributesAsync(long consumerId)
        {
            const string sql = @"
SELECT A.Id, A.NAME from Attribute A join ConsumerAttribute CA on A.Id = CA.AttributeId where CA.Active = 1 and A.Active = 1 and CA.ConsumerId = @consumerId";

            return await _dbContext.QueryAsync<ConsumerAttributeDto>(sql, new { consumerId });
        }

        public async Task SetConsumerAttributesAsync(long consumerId, List<long> attributes)
        {
            var consumerOldAttributes =  GetConsumerAttributesAsync(consumerId).Result;
            var oldAttributesIds = consumerOldAttributes.Select(l => l.Id).ToList();

            var removedAttributes = oldAttributesIds.Except(attributes).ToList();
            var addedAttributes = attributes.Except(oldAttributesIds).ToList();
            
            if(removedAttributes.Count > 0)
            {
                var removeSql = @"UPDATE [ConsumerAttribute] SET [ACTIVE] = 0 WHERE [ConsumerId] = " + consumerId + " and  [AttributeId] in (";
                
                foreach(var attribute in removedAttributes)
                {
                    removeSql += attribute +",";
                }   
                
                removeSql = removeSql.TrimEnd(',');
                removeSql += ");";
                
                await _dbContext.ExecuteAsync(removeSql);
            }
            
            if(addedAttributes.Count > 0)
            {
                var addedSql = @"INSERT INTO [ConsumerAttribute] ([ConsumerId], [AttributeId], [TimeStamp], [Active] ) VALUES ";
                
                foreach(var attribute in addedAttributes)
                {
                    addedSql += "(" + consumerId + ","+ attribute + " ,getdate(),1),";
                }
                
                addedSql = addedSql.TrimEnd(',');
                addedSql += ";";
                
                await _dbContext.ExecuteAsync(addedSql);
            }
        }
    }
}
