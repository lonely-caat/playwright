using System;
using System.Collections.Generic;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Models
{
    public class OrderDetailResponse
    {
        public long Id { get; set; }

        public IEnumerable<InstallmentResponse> Installments { get; set; }

        public decimal AmountPaid { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime? CaptureDate { get; set; }

        public decimal CapturedAmount { get; set; }

        public decimal FeeAmount { get; set; }

        public string MerchantOrderId { get; set; }

        public bool InStore { get; set; }

        public int TotalInstallments { get; set; }

        public string FeeFreePeriod { get; set; }

        public string RepaymentFrequency { get; set; }

        public decimal ArrearsAmount { get; set; }

        public string Status { get; set; }
    }
}
