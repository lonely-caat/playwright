using System;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Models
{
    public class DigitalWalletTokenResponse
    {
        public DateTime CreatedTime { get; set; }

        public DeviceResponse Device { get; set; }

        public TokenServiceProviderResponse TokenServiceProvider { get; set; }

        public string FulfillmentStatus { get; set; }

        public string IssuerEligibilityDecision { get; set; }

        public DateTime LastModifiedTime { get; set; }

        public string State { get; set; }

        public string Token { get; set; }
    }
}
