namespace Zip.Api.CustomerSummary.Domain.Entities.Statements
{
    public enum MessageTypes
    {
        None = 0,

        LmsCreateClient = 1,

        LmsCreateAccount = 2,

        LmsAddEstablishmentFee = 3,

        LmsCreateTransaction = 4,

        LmsCompleted = 5,

        ConsumerActivateProfile = 7,

        ConsumerContract = 8,

        LmsCreateRepaymentSchedule = 9,

        ConsumerContractUnregulated = 10,

        ConsumerInvite = 11,

        ConsumerResumeApplication = 12,

        ConsumerPaymentSync = 13,

        ConsumerBlackList = 14,

        OrderCreated = 50,

        ContractDeposit = 60,

        BatchPaymentSent = 70,

        BatchPaymentStarted = 71,

        BatchPaymentFinished = 72,

        EmailMerchantInvoice = 100,

        EmailConsumerStatement = 101,

        EmailForgottenPassword = 102,

        EmailConfirmation = 103,

        EmailSetPassword = 104,

        EmailVerification = 105,

        ArrearsSmsNotification = 200,

        ArrearsEmailNotification = 201,

        PaymentFailureNotification = 202,

        // merchant branch level notifications
        NotificationMerchantBranchShoppingCartStatusChanged = 300,

        NotificationMerchantBranchCreditProfileStatusChanged = 301,

        Shopify = 400,

        // administrator level notifications
        NotificationAdministratorCreditLimitUpdateStatusChanged = 500,

        NotificationAdministratorFraudTransactionDetected = 501,

        NotificationAdministratorDecisioningError = 502,

        // consumer level notifications
        NotificationConsumerCreditLimitUpdateStatusChanged = 600,

        NotificationConsumerTermsAccepted = 601,

        NotificationConsumerActivated = 602,

        NotificationConsumerPurchase = 603,

        NotificationConsumerDeclined = 602,

        NotificationConsumerApproved = 603,

        SmsNotificationConsumerActivated = 604,

        NotificationConsumerPaymentRequired = 605,

        //accounting
        MerchantFeeAccrual = 700,

        EstablishmentFeeAccrual = 701,

        RefundedMerchantFeeAccrual = 702,

        // zipPay notifications
        ZipPayRepaymentMethodNotSetReminder = 800,

        Failure = 1000,

        Retry = 1001
    }
}
