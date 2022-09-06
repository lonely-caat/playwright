using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Zip.Api.CustomerSummary.Domain.Entities.Payments
{
    public class Repayment
    {
        public long Id { get; set; }

        public long AccountId { get; set; }

        public decimal Amount { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Frequency Frequency { get; set; }

        public DateTime StartDate { get; set; }

        public string ChangedBy { get; set; }
    }

    public class NextPayment
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public Frequency Frequency { get; set; }
        public DateTime? StartDate { get; set; }
        public decimal? Amount { get; set; }
    }
}
