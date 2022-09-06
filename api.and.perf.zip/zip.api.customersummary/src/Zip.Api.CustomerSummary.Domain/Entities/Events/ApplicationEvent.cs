using System;

namespace Zip.Api.CustomerSummary.Domain.Entities.Events
{
    public class ApplicationEvent
    {
        public long Id { get; set; }

        public Guid EventId { get; set; }

        public string AggregateType { get; set; }

        public string AggregateId { get; set; }

        public string EventType { get; set; }

        public string Payload { get; set; }

        public DateTime CreatedTimestamp { get; set; }

        public byte[] RowVersion { get; set; }

        public bool Published { get; set; }
    }
}
