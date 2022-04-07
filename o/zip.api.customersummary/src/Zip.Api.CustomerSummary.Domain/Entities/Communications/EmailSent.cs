using System;
using Newtonsoft.Json;

namespace Zip.Api.CustomerSummary.Domain.Entities.Communications
{
    public class EmailSent
    {
        [JsonProperty("ConsumerId")]
        public long ConsumerId { get; set; }

        [JsonProperty("EmailType")]
        public string EmailType { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("CreationDate")]
        public DateTime CreatedDateTime { get; set; }

        [JsonProperty("Success")]
        public bool Success { get; set; }
    }
}
