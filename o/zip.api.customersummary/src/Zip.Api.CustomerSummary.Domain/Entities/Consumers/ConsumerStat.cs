using System;

namespace Zip.Api.CustomerSummary.Domain.Entities.Consumers
{
    public class ConsumerStat
    {
        public long Id { get; set; }

        public long ConsumerId { get; set; }

        public int SmsVerificationRetryCount { get; set; }

        public int SmsSendRetryCount { get; set; }

        public DateTime VerificationTimeStamp { get; set; }

        public DateTime SendTimeStamp { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
