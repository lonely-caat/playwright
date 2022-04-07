using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.ApplicationEvent;
using Zip.Api.CustomerSummary.Infrastructure.Services.ApplicationEventService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.Mocks.Services
{
    public class MockApplicationEventService : IApplicationEventService
    {
        public Task AddApplicationEventAndPublish<T>(T message, string aggregateId, AggregateEventType type = AggregateEventType.MerchantAccount) where T : Event
        {
            return Task.FromResult(0);
        }
    }
}
