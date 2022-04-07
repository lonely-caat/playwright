using System.Net;
using Newtonsoft.Json;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Response.PayOrder
{
    public class PayOrderInnerResponse
    {
        public HttpStatusCode Code { get; set; }

        public string Message { get; set; }

        [JsonProperty("success")]
        public bool IsSuccess { get; set; }
    }
}   
