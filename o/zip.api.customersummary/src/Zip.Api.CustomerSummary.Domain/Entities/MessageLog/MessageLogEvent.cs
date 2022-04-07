namespace Zip.Api.CustomerSummary.Domain.Entities.MessageLog
{
    public partial class MessageLogEvent
    {
        public long Id { get; set; }

        public long MessageLogId { get; set; }

        public MessageLogStatus Status { get; set; }

        public string Detail { get; set; }

        public System.DateTime TimeStamp { get; set; }

        public virtual MessageLog MessageLog { get; set; }
    }
}
