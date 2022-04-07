using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.CoreService.Models
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class CoreResetPasswordModel
    {
        public string Email { get; set; }
    }
}