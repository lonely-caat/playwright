using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Zip.Api.CustomerSummary.Domain.Entities.Consumers
{
    public class Attribute
    {
        public long Id { get; set; }

        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AttributeType Type { get; set; }

        public System.DateTime TimeStamp { get; set; }

        public bool Active { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AttributeCategory Category { get; set; }
    }
}
