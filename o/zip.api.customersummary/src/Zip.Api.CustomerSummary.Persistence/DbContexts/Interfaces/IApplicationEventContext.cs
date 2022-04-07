using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Events;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces
{
    public interface IApplicationEventContext
    {
        Task<ApplicationEvent> CreateAsync(ApplicationEvent @event);
        Task MarkAsUnpublishedAsync(long eventId);
        Task<ApplicationEvent> GetAsync(long eventId);
    }
}