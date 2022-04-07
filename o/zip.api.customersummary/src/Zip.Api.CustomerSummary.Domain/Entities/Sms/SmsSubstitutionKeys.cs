namespace Zip.Api.CustomerSummary.Domain.Entities.Sms
{
    public static class SmsSubstitutionKeys
    {
        public static string AccountUrl { get; } = "{account-url}";

        public static string PayNowUrl { get; } = "{payNowUrl}";

        public static string AmountDue { get; } = "{amount-due}";

        public static string Classification { get; } = "{classification}";

        public static string DueDate { get; } = "{due-date}";

        public static string DueDays { get; } = "{due-days}";

        public static string FirstName { get; } = "{first-name}";

        public static string LateFee { get; } = "{late-fee}";

        public static string MinimumRepayment { get; } = "{min-repayment}";

        public static string MonthlyFee { get; } = "{monthly-fee}";
    }
}
