using System;

namespace Zip.Api.CustomerSummary.Domain.Entities.Tango
{
    public class LoanArrearsAccount
    {
        public string AccountHash { get; set; }

        public decimal ArrearsBalanceAsAt { get; set; }

        public string BusinessCode { get; set; }

        public int? DaysInArrearsAsAt { get; set; }

        public string LoanStatus { get; set; }

        public decimal NetBalance { get; set; }

        public decimal NetBalanceAsAt { get; set; }

        public decimal? ContractualAmountDueAsAt { get; set; }

        public bool HoldProcessing { get; set; }

        public string GetLoanStatus()
        {
            var status = "Not Set";

            if (LoanStatus == null)
            {
                return status;
            }

            switch (LoanStatus.ToUpperInvariant())
            {
                case "A":
                    status = "Active";
                    break;
                
                case "C":
                    status = "Closed";
                    break;
                
                case "":
                case " ":
                    status = "Creating";
                    break;
                
                default:
                    status = LoanStatus;
                    break;
            }

            return status;
        }

        public bool IsAccountReady()
        {
            // return account ready for either Active, Closed or Lost
            return LoanStatus == "A" || LoanStatus == "C" || LoanStatus == "L";
        }

        public bool IsAccountClosed()
        {
            return !string.IsNullOrEmpty(LoanStatus) &&
                    LoanStatus.Equals("C", StringComparison.OrdinalIgnoreCase);
        }
    }
}
