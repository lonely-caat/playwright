using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.MerchantDashboardService.Models.Responses
{
    public class OrderSummaryResponse
    {
        public long OrderId { get; set; }

        public long MerchantId { get; set; }

        public string MerchantOrderId { get; set; }

        [JsonProperty("ReceiptNumber")]
        public string OperationRequestId { get; set; }

        public DateTime OrderTimeStamp { get; set; }

        public string OrderReference { get; set; }

        public string MerchantName { get; set; }

        public string BranchName { get; set; }

        [JsonProperty("Amount")]
        public decimal TotalAmount { get; set; }

        public string ShippingAddress { get; set; }

        [JsonProperty("InterestFreePeriod")]
        public int? InterestFreeMonths { get; set; }

        public IEnumerable<TransactionHistoryItem> TransactionHistoryItems { get; set; }
    }
}