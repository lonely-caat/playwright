using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Zip.Api.CustomerSummary.Domain.Entities.Products
{
    public class Product
    {
        public long Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ProductClassification Classification { get; set; }

        public string ClassificationDisplayName { get; set; }

        public string CountryId { get; set; }

        public int Status { get; set; }
    }
}
