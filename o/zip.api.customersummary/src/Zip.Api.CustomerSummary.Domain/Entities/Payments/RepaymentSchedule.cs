using System;

namespace Zip.Api.CustomerSummary.Domain.Entities.Payments
{
    public class RepaymentSchedule
    {
        public string Description { get; set; }

        public decimal? MonthlyFee { get; set; }

        public decimal? MinimumMonthlyRepayment { get; set; }

        public decimal? MinimumRepaymentPercentage { get; set; }

        public decimal? EstablishmentFee { get; set; }

        public decimal? ContractualAmount { get; set; }

        public DateTime? ContractualDate { get; set; }

        public DateTime? ArrearsHoldDate { get; set; }
    }
}
