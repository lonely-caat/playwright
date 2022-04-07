using System;

namespace Zip.Api.CustomerSummary.Domain.Entities.Accounts
{
    public class LmsAccountDto
    {
        public string CreditStatus { get; set; }

        public decimal? CreditLimit { get; set; }

        public decimal? InterestFreeBalance { get; set; }

        public decimal? CurrentBalance { get; set; }

        public decimal? AvailableFunds { get; set; }

        public decimal? ArrearsBalance { get; set; }

        public decimal? PendingBalance { get; set; }

        public int? DaysInArrears { get; set; }

        public long? AccountWithCheckDigit { get; set; }

        public DateTime? ContractualDate { get; set; }

        public decimal? ContractualAmount { get; set; }

        public DateTime? NextRepaymentDate { get; set; }
    }
}
