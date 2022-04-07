using System;

namespace Zip.Api.CustomerSummary.Domain.Entities.ApplicationEvent.Interfaces
{
    public interface IEvent
    {
        Guid Id { get; }

        DateTime TimeStamp { get; }
        string RaisingComponent { get; }
        string Version { get; }
        string SourceIp { get; }
        string Tenant { get; set; }
        string Conversation { get; set; }
    }
}
