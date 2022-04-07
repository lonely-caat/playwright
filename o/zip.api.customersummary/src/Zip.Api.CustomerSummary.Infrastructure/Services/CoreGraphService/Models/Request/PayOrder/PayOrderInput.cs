using Newtonsoft.Json;
using System;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Request.PayOrder
{
    public class PayOrderInput
    {
        public PayOrderInput(
            string accountId,
            Guid customerId,
            long orderId,
            decimal amount)
        {
            AccountId = accountId;
            CustomerId = customerId;
            OrderId = orderId;
            Amount = amount;
        }

        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        [JsonProperty("customerId")]
        public Guid CustomerId { get; set; }

        [JsonProperty("orderId")]
        public long OrderId { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}
