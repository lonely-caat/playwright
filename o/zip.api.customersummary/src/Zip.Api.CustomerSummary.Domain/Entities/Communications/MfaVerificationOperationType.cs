namespace Zip.Api.CustomerSummary.Domain.Entities.Communications
{
    public enum MfaVerificationOperationType
    {
        ConsumerOnboarding = 1,

        ConsumerCheckout = 2,

        MerchantSignUp = 3,

        AdminConsumerIdentification = 4,

        ConsumerDetailsUpdate = 5,

        Instore = 6
    }
}
