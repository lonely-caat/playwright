using System;
using Zip.Api.CustomerSummary.Domain.Entities.Transactions;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.MerchantDashboardService.Models
{
    public class TransactionHistoryItem
    {
        public long Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public TransactionType TransactionType { get; set; }

        public string Comments { get; set; }

        public string ChangedBy { get; set; }

        public string MerchantId { get; set; }

        public string TransactionId { get; set; }

        public long? DisbursementId { get; set; }

        public decimal? Amount { get; set; }

        public int? InterestFreePeriod { get; set; }

        public bool IsCancelledForCapturedTransaction { get; set; }

        public long? CapturedTransactionHistoryId { get; set; }
    }
}
