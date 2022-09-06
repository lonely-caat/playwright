namespace Zip.Api.CustomerSummary.Domain.Entities.MessageLog
{
    public enum MessageLogType : short
    {
        // Application
        Generic = 0,
        InProgress = 1,
        InProgressSecondary = 2,
        ActivationRemark = 3,
        BankStatementRequest = 4,

        // Consumer
        Statement = 100,
        Welcome = 101,
        Cancelled = 102,
        Approved = 103,
        Declined = 104,
        Activated = 105,
        RegistrationReminder = 106,
        RepaymentDetails = 107,
        ResetPassword = 108,
        NewApplication = 109,
        Order = 110,
        AccountClosed = 111,
        Invitation = 112,
        ApprovedWithOrder = 113,
        EmailConfirmation = 114,
        EmailVerification = 115,
        PayOutQuote = 116,
        ClosureLetter = 117,

        // Consumer Payment Arrears
        PaymentArrearsFailureNotice = 120,
        PaymentArrearsOverdueNotice = 121,
        PaymentArrearsOverdueNoticeStrong = 122,
        PaymentArrearsFinalNotice = 123,
        PaymentArrearsConfiguredSms = 124,

        // Arrears Notices
        ArrearsNoticeSection88 = 126,
        ArrearsNoticeSection21D = 127,

        // Merchant
        Invoice = 200,
        MerchantActivation = 201,
        MerchantResetPassword = 202,
        Disbursement = 203,
        MerchantSetPassword = 204,

        // Operation Request
        Resume = 300,

        // System
        MerchantEnquiry = 400,
        CustomerEnquiry = 401,
        EcommerceApiError = 402,
        BatchPaymentFileProcessing = 403,

        // merchant branch level notifications
        MerchantBranchShoppingCartStatusUnderReview = 500,
        MerchantBranchShoppingCartStatusContractPending = 501,
        MerchantBranchShoppingCartStatusDeclined = 502,
        MerchantBranchShoppingCartStatusAuthorised = 503,
        MerchantBranchShoppingCartStatusCompleted = 504,
        MerchantBranchShoppingCartStatusRefunded = 505,
        MerchantBranchShoppingCartStatusCancelled = 506,
        MerchantBranchShoppingCartStatusDepositRequired = 507,
        MerchantBranchShoppingCartStatusPartiallyCaptured = 508,
        MerchantBranchCreditProfileStateTypeApproved = 600,
        MerchantBranchCreditProfileStateTypeRegistered = 601,
        MerchantBranchCreditProfileStatusChanged = 602,
        MerchantBranchCreditProfileStatusChangedWithOrder = 603,

        // administrator level notifications
        AdministratorCreditLimitUpdateSubmitted = 700,
        AdministratorFraudTransactionDetected = 701,
        AdministratorDecisioningError = 702,

        // consumer level notifications
        ConsumerCreditLimitUpdateSubmitted = 800,
        ConsumerCreditLimitUpdateApproved = 801,
        ConsumerCreditLimitUpdateDeclined = 802,
        ConsumerCreditLimitUpdateAccepted = 803,
        ConsumerTermsAccepted = 804,
        ConsumerTermsAcceptedWithDirectDebit = 805,
        ConsumerTermsAcceptedWithNoDirectDebit = 806,
        ConsumerActivated = 807,
        ConsumerPurchase = 808,
        ConsumerFirstPurchase = 809,
        ConsumerPurchaseSms = 810,
        ConsumerFirstPurchaseSms = 811,
        ConsumerPaymentRequired = 812,

        // zipPay notifications
        ZipPayRepaymentMethodNotSetReminder = 1000,

        // zipBill notifications
        ZipBillsList = 1100,
        FundingProgramError = 1200,

        // BankStatement Application Email Requests
        Timeout = 1300,
        NewBank = 1301,
        MultipleAccounts = 1302,
        CurrentData = 1303,
        ScreenshotsOrTransactionListings = 1304,
        InternetBankingCompulsory = 1305,
        ZipPayDecline = 1306,
        CancellationWarning = 1307,
        IdentityConfirmation = 1308,
    }
}
