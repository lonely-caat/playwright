using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using Zip.Services.Accounts.Contract.Order;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Models
{
    public class InstallmentResponse
    {
        public decimal Amount { get; set; }

        public decimal AmountWithoutFees => Fee > 0 ? Amount - Fee : Amount;

        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentStatus Status { get; set; }

        public DateTime Date { get; set; }

        public decimal Fee { get; set; }

        public decimal TotalPaidAmount { get; set; }

        public bool IsPaidOff { get; set; }

        public int DaysPastDue =>
            Status == PaymentStatus.Requested || Status == PaymentStatus.Declined ? (DateTime.Now - Date).Days : 0;
    }
}