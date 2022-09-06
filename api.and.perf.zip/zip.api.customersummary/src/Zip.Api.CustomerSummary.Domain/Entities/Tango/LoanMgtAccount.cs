using System;
using System.Globalization;

namespace Zip.Api.CustomerSummary.Domain.Entities.Tango
{
    public class LoanMgtAccount : LoanArrearsAccount
    {
        private static readonly string[] DateFormats = { "dd/MM/yyyy", "dd-MM-yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "yyyyMMdd" };

        private DateTime? _arrearsStartDate;

        public int Term { get; set; }

        public int? AccountWithCheckDigit { get; set; }

        public int? OriginalTerm { get; set; }

        public string DateEntered { get; set; }

        public string SettlementStatus { get; set; }

        public string SettlementDate { get; set; }

        public string GeneralPurpose1 { get; set; }

        public decimal InterestRate { get; set; }

        public decimal LoanLimit { get; set; }

        public string InstalmentDate { get; set; }

        public decimal PurchasePrice { get; set; }

        public string LastInterestRunDate { get; set; }

        public decimal PrePayment { get; set; }

        public decimal ResidualValue { get; set; }

        public decimal AvailableLoanLimit { get; set; }

        public decimal? InterestFreeBalance { get; set; }

        public int? ArrearsDays { get; set; }

        public decimal? DirectDebitAmountDueAsAt { get; set; }

        public string ContractualNextDueDateAsAt { get; set; }

        public string DirectDebitNextDateDueAsAt { get; set; }

        public string DirectDebitFrequencyAsAt { get; set; }

        public string NextDue { get; set; }

        public string LastPaymentDate { get; set; }

        public decimal? PayoutQuote { get; set; }

        public DateTime? ArrearsStartDate
        {
            get
            {
                if (_arrearsStartDate != null || DaysInArrearsAsAt == null)
                {
                    return _arrearsStartDate;
                }

                _arrearsStartDate = DateTime.Now.AddDays(DaysInArrearsAsAt.Value * -1);

                return _arrearsStartDate;
            }
        }

        public DateTime? CachedDate { get; set; }

        public string GetContractualDueDate()
        {
            if (string.IsNullOrEmpty(ContractualNextDueDateAsAt))
            {
                return string.Empty;
            }

            if (!DateTime.TryParseExact(ContractualNextDueDateAsAt, DateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var value))
            {
                throw new FormatException($"String {ContractualNextDueDateAsAt} was not recognized as a valid DateTime");
            }
            
            return value.ToString(App.DateFormat);
        }

        public virtual bool IsAccountLocked()
        {
            return IsAccountInNoFurtherDrawdownState() || IsAccountInStopCredit() || IsAccountLost();
        }

        public bool IsAccountInNoFurtherDrawdownState()
        {
            return !string.IsNullOrEmpty(GeneralPurpose1) &&
                    GeneralPurpose1.Equals(LoanMgt.NoFurtherDrawDownStatus, StringComparison.OrdinalIgnoreCase);
        }

        public string GetAccountStatus()
        {
            if (IsAccountInNoFurtherDrawdownState() || IsAccountInStopCredit() || IsAccountInLossRecovery())
            {
                return GeneralPurpose1;
            }

            if (IsAccountLost())
            {
                return LoanMgt.LostStatus;
            }

            if (IsAccountClosed())
            {
                return LoanMgt.ClosedStatus;
            }

            return GeneralPurpose1 ?? string.Empty;
        }

        public DateTime? GetContractualDueDateTime()
        {
            if (!DateTime.TryParseExact(ContractualNextDueDateAsAt, DateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var value))
            {
                return null;
            }

            return value;
        }

        public string GetDirectDebitDueDate()
        {
            if (string.IsNullOrEmpty(DirectDebitNextDateDueAsAt))
            {
                return string.Empty;
            }

            if (!DateTime.TryParseExact(DirectDebitNextDateDueAsAt, DateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var value))
            {
                throw new FormatException($"String {DirectDebitNextDateDueAsAt} was not recognized as a valid DateTime");
            }

            return value.ToString(App.DateFormat);
        }

        public DateTime? GetDirectDebitDueDateTime()
        {
            if (string.IsNullOrEmpty(DirectDebitNextDateDueAsAt))
            {
                return null;
            }

            if (!DateTime.TryParseExact(ContractualNextDueDateAsAt, DateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var value))
            {
                return null;
            }
            
            return value;
        }

        public Frequency GetFrequency()
        {
            if (string.IsNullOrEmpty(DirectDebitNextDateDueAsAt))
            {
                return Frequency.Monthly;
            }

            var frequency = Frequency.Monthly;
            
            switch (DirectDebitFrequencyAsAt.ToUpperInvariant())
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

        public void ChangeAccountLockedStatus(bool lockAccount)
        {
            GeneralPurpose1 = lockAccount ? LoanMgt.NoFurtherDrawDownStatus : LoanMgt.OperationalStatus;
        }

        public bool IsAccountAvailable()
        {
            return !string.Equals(AccountHash, "NOT AVAILABLE");
        }

        public virtual bool IsAccountInArrears => ArrearsBalanceAsAt > 0;

        public decimal PendingBalance { get; set; }

        private bool IsAccountInLossRecovery()
        {
            return !string.IsNullOrEmpty(GeneralPurpose1) &&
                    GeneralPurpose1.Equals(LoanMgt.LossRecoveryStatus, StringComparison.OrdinalIgnoreCase);
        }

        private bool IsAccountInStopCredit()
        {
            return !string.IsNullOrEmpty(GeneralPurpose1) &&
                    GeneralPurpose1.Equals(LoanMgt.StopCreditStatus, StringComparison.OrdinalIgnoreCase);
        }

        private bool IsAccountLost()
        {
            // mike greer - 5 Oct 2017 - All new write offs will now have a GeneralPurpose1 of "Recovery Ledger" or "Loss Recovery" and AccountStatus of ChargedOff

            var isLostBeforeOct2017 = !string.IsNullOrEmpty(LoanStatus) &&
                    LoanStatus.Equals("L", StringComparison.OrdinalIgnoreCase);

            var isLostAfterOct2017 = !string.IsNullOrWhiteSpace(GeneralPurpose1) &&
                    GeneralPurpose1.Equals(LoanMgt.LostStatus2, StringComparison.OrdinalIgnoreCase);

            return isLostBeforeOct2017 || isLostAfterOct2017 || IsAccountInLossRecovery();
        }
    }
}
