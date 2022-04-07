using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Attribute = Zip.Api.CustomerSummary.Domain.Entities.Consumers.Attribute;

namespace Zip.Api.CustomerSummary.Api.Test.Mocks.DbContexts
{
    public class MockAttributeContext : IAttributeContext
    {
        public Task<IEnumerable<Attribute>> GetAttributesAsync()
        {
            var result = new List<Attribute>()
            {
                new Attribute()
                {
                    Id = 1,
                    Name = "DECEASED",
                    Active = true, Category = AttributeCategory.FraudRisk, 
                    TimeStamp = DateTime.Today, Type = AttributeType.ConsumerCollection
                }
            };

            return Task.FromResult<IEnumerable<Attribute>>(result);
        }

        public Task<IEnumerable<ConsumerAttributeDto>> GetConsumerAttributesAsync(long consumerId)
        {
            var result = consumerId == int.MaxValue ? null : new List<ConsumerAttributeDto>()
            {
                new ConsumerAttributeDto()
                {
                    Name = "test attribute",
                    Id = 1
                }
            };

            return Task.FromResult<IEnumerable<ConsumerAttributeDto>>(result);
        }

        public Task SetConsumerAttributesAsync(long consumerId, List<long> attributes)
        {
            if(consumerId == int.MaxValue)
                throw new Exception("test exception");

            return Task.CompletedTask;
        }
    }
}