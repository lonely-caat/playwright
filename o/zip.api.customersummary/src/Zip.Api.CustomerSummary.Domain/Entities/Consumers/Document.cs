using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Zip.Api.CustomerSummary.Domain.Entities.Consumers
{
    public class Document
    {
        public long Id { get; set; }

        public long ConsumerId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public DocumentType DocumentType { get; set; }

        public string FriendlyName { get; set; }

        public string Value1 { get; set; }

        public string Value2 { get; set; }
    }
}
