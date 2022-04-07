using Newtonsoft.Json;
using System;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Request.PayOrder
{
    public class PayOrderRequest
    {
        public PayOrderRequest(
            string query,
            string accountId,
            Guid customerId,
            long orderId,
            decimal amount)
        {
            Query = query;
            Variables = new PayOrderRequestVariables(accountId, customerId, orderId, amount);
        }

        [JsonProperty("query")]
        public string Query { get; set; }
        
        [JsonProperty("variables")]
        public PayOrderRequestVariables Variables { get; set; }
    }
}
