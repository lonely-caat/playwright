using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Zip.Api.CustomerSummary.Domain.Entities.Accounts
{
    public class CreditProfileState
    {
        public long Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CreditProfileStateType CreditStateType { get; set; }

        public System.DateTime TimeStamp { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CreditProfileStateReasonCode? ReasonCode { get; set; }

        public long CreditProfileId { get; set; }

        public string Comments { get; set; }

        public string ChangedBy { get; set; }
    }
}
