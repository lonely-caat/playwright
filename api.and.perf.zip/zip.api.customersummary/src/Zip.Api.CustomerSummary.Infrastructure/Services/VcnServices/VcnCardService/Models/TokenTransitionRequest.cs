using System;
using Newtonsoft.Json;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Models
{
    public class TokenTransitionRequest
    {
        public TokenTransitionRequest(Guid token, string provider, string state, string reasonCode)
        {
            DigitalWalletToken = token;
            Provider = provider;
            State = state;
            ReasonCode = reasonCode;
        }

        public TokenTransitionRequest()
        {
        }
        
        [JsonProperty(PropertyName = "provider")]
        public string Provider { get; set; }

        [JsonProperty(PropertyName = "digitalWalletToken")]
        public Guid DigitalWalletToken { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "reasonCode")]
        public string ReasonCode { get; set; }
    }
}
