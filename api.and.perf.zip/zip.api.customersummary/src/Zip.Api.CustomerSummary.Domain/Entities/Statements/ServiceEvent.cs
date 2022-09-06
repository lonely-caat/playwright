namespace Zip.Api.CustomerSummary.Domain.Entities.Statements
{
    public partial class ServiceEvent
    {
        public long Id { get; set; }

        public System.Guid MessageId { get; set; }

        public MessageTypes Type { get; set; }

        public string Reference { get; set; }

        public MessageCategory Category { get; set; }

        public MessageStatus Status { get; set; }

        public System.DateTime TimeStamp { get; set; }

        public string Result { get; set; }
    }
}
