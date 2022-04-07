using System;

namespace Zip.Api.CustomerSummary.Domain.Entities.MessageLog
{
    public class MessageLog
    {
        public long Id { get; set; }

        public MessageLogDeliveryMethod DeliveryMethod { get; set; }

        public MessageLogCategory Category { get; set; }

        public MessageLogType Type { get; set; }

        public long ReferenceId { get; set; }

        public string Subject { get; set; }

        public DateTime TimeStamp { get; set; }

        public string MessageId { get; set; }

        public string Content { get; set; }
    }
}
