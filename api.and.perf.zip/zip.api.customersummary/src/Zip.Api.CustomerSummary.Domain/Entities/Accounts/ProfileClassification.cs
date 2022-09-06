using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Zip.Api.CustomerSummary.Domain.Entities.Accounts
{
    public partial class ProfileClassification
    {
        public long Id { get; set; }

        public string Description { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ProfileStateType Type { get; set; }

        public System.DateTime TimeStamp { get; set; }

        public bool Active { get; set; }
    }
}
