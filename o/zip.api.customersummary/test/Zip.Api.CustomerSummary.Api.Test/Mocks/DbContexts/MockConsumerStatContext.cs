using System;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.Mocks.DbContexts
{
    public class MockConsumerStatContext : IConsumerStatContext
    {
        public Task<ConsumerStat> GetByConsumerIdAsync(long consumerId)
        {
            var result = consumerId == int.MaxValue ? null : new ConsumerStat() { ConsumerId = consumerId };
            return Task.FromResult(result);
        }

        public Task SaveAsync(ConsumerStat consumerStat)
        {
            if(consumerStat.ConsumerId == int.MaxValue)
                throw new Exception("test exception");

            consumerStat.Id = 1;
            return Task.CompletedTask;
        }

        public Task<ConsumerStat> GetAsync(long id)
        {
            var result = id == int.MaxValue ? null : new ConsumerStat() { ConsumerId = 1, Id = id};
            return Task.FromResult(result);
        }
    }
}