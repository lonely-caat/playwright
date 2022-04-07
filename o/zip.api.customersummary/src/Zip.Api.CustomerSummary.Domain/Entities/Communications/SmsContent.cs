using System;

namespace Zip.Api.CustomerSummary.Domain.Entities.Communications
{
    public class SmsContent
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool Active { get; set; }
    }
}
