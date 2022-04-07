using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.ApplicationEvent;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.ApplicationEventService.Interfaces
{
    public interface IApplicationEventService
    {
        Task AddApplicationEventAndPublish<T>(
            T message,
            string aggregateId,
            AggregateEventType type = AggregateEventType.MerchantAccount) where T : Event;
    }
}