using System;

namespace Zip.Api.CustomerSummary.Domain.Entities.Tango
{
    public class LoanMgtRepaymentScheduleVariation
    {
        public string AccountHash { get; set; }
        public long DirectDebitId { get; set; }
        public bool? IsDistinct { get; set; }
        public long LoanAccountNumber { get; set; }
        public decimal OverrideRepaymentAmount { get; set; }
        // OverrideRepaymentMethod can either be not supplied, empty or "W", "F", or "M". e.g. Weekly, Fortnightly or Monthly
        public string OverrideRepaymentMethod { get; set; }
        public DateTime? RepaymentVariationStart { get; set; }
        public DateTime? RepaymentVariationEnd { get; set; }
        // Always set precedence to True
        public bool? VariationHasPrecedence { get; set; }

        public LoanMgtRepaymentScheduleVariation()
        {
            // Y2.1K
            RepaymentVariationEnd = new DateTime(2099, 1, 1);
        }

        public Frequency GetFrequency()
        {
            var frequency = Frequency.Monthly;
            switch (OverrideRepaymentMethod)
            {
                case "W":
                    frequency = Frequency.Weekly;
                    break;
                case "F":
                    frequency = Frequency.Fortnightly;
                    break;
                case "M":
                    frequency = Frequency.Monthly;
                    break;
            }
            return frequency;
        }

        public static string GetFrequencyMethod(Frequency frequency)
        {
            var value = "";
            switch (frequency)
            {
                case Frequency.Weekly:
                    value = "W";
                    break;
                case Frequency.Fortnightly:
                    value = "F";
                    break;
                case Frequency.Monthly:
                    value = "M";
                    break;
            }
            return value;
        }

        public string SetFrequency(Frequency frequency)
        {
            OverrideRepaymentMethod = GetFrequencyMethod(frequency);
            return OverrideRepaymentMethod;
        }
    }
}
