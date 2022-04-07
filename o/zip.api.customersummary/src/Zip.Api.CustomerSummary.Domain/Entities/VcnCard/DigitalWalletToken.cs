using System;

namespace Zip.Api.CustomerSummary.Domain.Entities.VcnCard
{
    public class DigitalWalletToken
    {
        public DateTime CreatedTime { get; set; }

        public Device Device { get; set; }

        public TokenServiceProvider TokenServiceProvider { get; set; }

        public string FulfillmentStatus { get; set; }

        public string IssuerEligibilityDecision { get; set; }

        public DateTime LastModifiedTime { get; set; }

        public string State { get; set; }

        public string Token { get; set; }
    }
}
