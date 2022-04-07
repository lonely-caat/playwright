namespace Zip.Api.CustomerSummary.Domain.Entities.Sms
{
    public enum PaymentFailureType
    {
        NotApplicable,

        CardReferToCardIssuer,

        CardDeclined,

        CardInsufficientFunds,

        CardExpired,

        BankInsufficientFunds,

        BankIncorrectDetails,

        GenericFailure,

        Timeout,
    }
}
