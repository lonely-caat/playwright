using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Zip.Api.CustomerSummary.Api.Models
{
    [ExcludeFromCodeCoverage]
    public class ExceptionResponse
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
