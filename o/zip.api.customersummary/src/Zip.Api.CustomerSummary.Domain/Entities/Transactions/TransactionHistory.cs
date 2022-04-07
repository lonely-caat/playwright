using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Zip.Api.CustomerSummary.Domain.Entities.Transactions
{
    public class TransactionHistory
    {
        public long Id { get; set; }
        
        public long AccountId { get; set; }

        public long ConsumerId { get; set; }

        public long ThreadId { get; set; }

        public long OrderId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TransactionType Type { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ShoppingCartDetailStatus Status { get; set; }

        public DateTime TimeStamp { get; set; }

        public decimal Amount { get; set; }

        public string MerchantName { get; set; }
    }
}
