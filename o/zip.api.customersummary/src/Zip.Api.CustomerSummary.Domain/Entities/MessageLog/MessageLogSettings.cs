namespace Zip.Api.CustomerSummary.Domain.Entities.MessageLog
{
    public class MessageLogSettings
    {
        public MessageLogCategory Category { get; set; }

        public MessageLogDeliveryMethod DeliveryMethod { get; set; }

        public MessageLogType Type { get; set; }

        public MessageLogStatus Status { get; set; }
    }
}
