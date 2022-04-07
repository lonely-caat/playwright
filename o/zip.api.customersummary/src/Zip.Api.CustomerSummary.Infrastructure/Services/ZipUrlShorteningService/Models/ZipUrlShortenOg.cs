using Newtonsoft.Json;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.ZipUrlShorteningService.Models
{
    public class ZipUrlShortenOg
    {
        public string Title { get; set; }

        public string Description { get; set; }

        [JsonProperty(PropertyName = "site_name")]
        public string SiteName { get; set; }
    }
}