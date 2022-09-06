using System;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Statements;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces
{
    public interface IServiceEventContext
    {
        Task<ServiceEvent> GetAsync(MessageCategory category, MessageTypes type, string reference);

        Task CreateAsync(Guid messageId, MessageCategory category, MessageTypes type, string reference, MessageStatus status);
    }
}
