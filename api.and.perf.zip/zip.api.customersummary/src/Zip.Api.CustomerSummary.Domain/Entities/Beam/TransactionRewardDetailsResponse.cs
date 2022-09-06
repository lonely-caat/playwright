using System;
using System.Diagnostics.CodeAnalysis;

namespace Zip.Api.CustomerSummary.Domain.Entities.Beam
{
    [ExcludeFromCodeCoverage]
    public class TransactionRewardDetailsResponse
    {
        public string Title { get; set; }

        public string SubTitle { get; set; }

        public decimal Balance { get; set; }

        public string CampaignName { get; set; }

        public long ConsumerId { get; set; }

        public long ZipTransactionId { get; set; }

        public long Timestamp { get; set; }

        public DateTime DateTime => DateTimeOffset.FromUnixTimeMilliseconds(Timestamp).DateTime.ToLocalTime();
    }
}
