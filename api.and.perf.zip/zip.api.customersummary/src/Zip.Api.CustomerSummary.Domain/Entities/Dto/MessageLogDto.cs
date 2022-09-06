using System;
using Newtonsoft.Json;
using Zip.Api.CustomerSummary.Domain.Entities.MessageLog;

namespace Zip.Api.CustomerSummary.Domain.Entities.Dto
{
    public class MessageLogDto
    {
        [JsonIgnore]
        public MessageLog.MessageLog MessageLog { get; set; }

        [JsonIgnore]
        public MessageLogEvent MessageLogEvent { get; set; }

        public long Id => MessageLog.Id;

        public string DeliveryMethod => MessageLog.DeliveryMethod.ToString();

        public string Category => MessageLog.Category.ToString();

        public string Type => MessageLog.Type.ToString();

        public long ReferenceId => MessageLog.ReferenceId;

        public string Subject => MessageLog.Subject;

        public DateTime TimeStamp => MessageLog.TimeStamp;

        public string MessageId => MessageLog.MessageId;

        public string Content => MessageLog.Content;

        public string Status => MessageLogEvent.Status.ToString();
    }
}
