using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Infrastructure.Services.ApplicationEventService.Interfaces;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Mocks.Services
{
    public class MockEventBus : IEventBus
    {
        public Task PublishAsync<T>(T @event) where T:class
        {
            return Task.CompletedTask;
        }
    }
}
