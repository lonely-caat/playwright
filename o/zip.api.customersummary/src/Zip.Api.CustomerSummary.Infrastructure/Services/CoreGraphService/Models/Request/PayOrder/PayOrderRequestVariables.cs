using Newtonsoft.Json;
using System;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Request.PayOrder
{
    public class PayOrderRequestVariables
    {
        public PayOrderRequestVariables(
            string accountId,
            Guid customerId,
            long orderId,
            decimal amount)
        {
            Input = new PayOrderInput(accountId, customerId, orderId, amount);
        }

        [JsonProperty("input")]
        public PayOrderInput Input { get; set; }
    }
}
