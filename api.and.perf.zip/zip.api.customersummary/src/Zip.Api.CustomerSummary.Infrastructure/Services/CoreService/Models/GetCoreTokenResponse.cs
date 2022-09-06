using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.CoreService.Models
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class GetCoreTokenResponse
    {
        public string AccessToken { get; set; }

        public long ExpiresIn { get; set; }

        public string TokenType { get; set; }
    }
}