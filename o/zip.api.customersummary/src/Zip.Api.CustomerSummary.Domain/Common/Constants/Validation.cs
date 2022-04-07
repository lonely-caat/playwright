namespace Zip.Api.CustomerSummary.Domain.Common.Constants
{
    public static class Validation
    {
        public const string EmailRegex = @"^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$";
        public const string PhoneRegex = @"(^1300\d{6}$)|(^(1800|1900|1902)\d{6}$)|(^13\d{4}$)|(^(?:0)[2-478](?:[0-9]){8}$)|(^(?:\(0)[2-478]\)(?:[0-9]){8}$)";
        public const string DateRegex = @"^\d{2}\/\d{2}\/\d{4}$";
        public const string LegacyNameRegex = @"^[\p{L}\p{M}' \.\-]+$";
        public const string UrlRegex = @"^(http\:\/\/|https\:\/\/)?([a-z0-9][a-z0-9\-]*\.)+[a-z0-9][a-z0-9\-]*$";
        public const string SpecialCharsRegex = @"[^\w]+";
        public const string PostCodeRegex = @"^\d{4}$";
        public const string MedicareRegex = @"^\d{10}$";
        public const string AustralianMobilePhone = @"^04|^\+614([0-9]{8})";
        public const string NameRegex = @"^[a-zA-Z'\s\-]{2,}$";
        public const string BsbRegex = @"^\d{6}$";
        public const string AccountNumberRegex = @"^\d{1,10}$";
        public const string AbnRegex = @"^\d{11}$";
    }
}
