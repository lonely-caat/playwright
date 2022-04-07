using System.Threading.Tasks;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.ApplicationEventService.Interfaces
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T @event) where T : class;
    }
}
