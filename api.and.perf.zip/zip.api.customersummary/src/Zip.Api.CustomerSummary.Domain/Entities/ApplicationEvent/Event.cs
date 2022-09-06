using System;
using Zip.Api.CustomerSummary.Domain.Entities.ApplicationEvent.Interfaces;

namespace Zip.Api.CustomerSummary.Domain.Entities.ApplicationEvent
{
    public abstract class Event : IEvent
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime TimeStamp { get; set; } = DateTime.Now;

        public string RaisingComponent { get; set; }

        public string Version { get; }

        public string SourceIp { get; }

        public string Tenant { get; set; }

        public string Conversation { get; set; }

        protected Event()
        {
        }
    }
}
