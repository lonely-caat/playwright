using System;

namespace Zip.Api.CustomerSummary.Domain.Entities.Beam
{
    public class RewardActivity
    {
        public string Title { get; set; }

        public string Subtitle { get; set; }

        public decimal Balance { get; set; }
        
        public decimal RunningTotal { get; set; }

        public string CampaignName { get; set; }

        public long ConsumerId { get; set; }

        public long ZipTransactionId { get; set; }

        public long Timestamp { get; set; }

        public DateTime DateTime => DateTimeOffset.FromUnixTimeMilliseconds(Timestamp).DateTime.ToLocalTime();
    }
}
