using Newtonsoft.Json;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Response.PayOrder
{
    public class PayOrderData
    {
        [JsonProperty("makeRepaymentForOrder")]
        public PayOrderInnerResponse InnerResponse { get; set; }
    }
}
