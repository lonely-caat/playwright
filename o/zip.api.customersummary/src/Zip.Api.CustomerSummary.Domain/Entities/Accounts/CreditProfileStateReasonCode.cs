namespace Zip.Api.CustomerSummary.Domain.Entities.Accounts
{
    public enum CreditProfileStateReasonCode
    {
        None = 0,

        PreviousDeclineLessThan6MTHS = 1,

        MultipleAttempts = 2,

        AgeUnder18 = 3,

        FieldValidation = 4,

        UnEmployed = 5,

        InsufficientIncome = 6,

        LargerOtherIncome = 7,

        NoCapacity = 8,

        AgeOver70 = 9,

        ResidentialStablity = 10,

        EmploymentStability = 11,

        Student = 12,

        ProfileStability = 13,

        BCAError = 14,

        EmpStatusShortTerm = 15,

        ThreatmetrixDeclined = 16,

        StatementUploaded = 17,

        StatementAssessment = 18,

        StatementNotFound = 19,

        StatementSkipped = 20,

        StatementWrongFormat = 21,

        ManualLargeDSS = 22,

        ManualBalanceOverdrawn = 23,

        ManualDishonours = 24,

        ExcessiveRiskProfile = 25,

        FraudIdentified = 26,

        VedaConnectFailed = 27,

        PossibleDuplicateApplication = 28,

        IDMatrixFailed = 29,

        IDMatrixRefer = 30,

        BureauPoorCreditHistory = 31,

        PolicyFail = 32,

        BureauNewFile = 33,

        BureauYoungFile = 34,

        BureauPossibleMatch = 35,

        BureauNumberofEnquiries = 36,

        BureauAdverse = 37,

        DuplicateFullName = 38,

        DuplicateLastNameAndDOB = 39,

        DuplicateDocument = 40,

        SimilarAddress = 41,

        RequestBillingAddressExists = 42,

        RequestShippingAddressExists = 43,

        ApplicationModificationEmailAddress = 44,

        ApplicationModificationName = 45,

        ApplicationModificationMobilePhone = 46,

        ApplicationModificationAddress = 47,

        DeviceIdExists = 48,

        SmartIdExists = 49,

        IpAddressExists = 50,

        EngineConfigurationError = 51,

        InvalidEmail = 52,

        StatementAccountsNotFound = 53,

        EngineRuleEvaluationError = 54,

        EmploymentOther = 55,

        EmploymentRetired = 56,

        VedaScore = 57,

        UnemployedAndCondition = 58,

        Social = 59,

        Fraud = 60,

        DuplicateEmailAddress = 61,

        DuplicateMobilePhone = 62,

        ExistingAlternateAccountWithProfileState = 63,

        EmailSignup = 64,

        AuthorizeRequestConsumerAccountUnreliable = 65,

        FuzzyFirstNameMatch = 66,

        FuzzyFirstNameMatchForEmailSignUp = 67,

        FuzzyLastNameMatch = 68,

        FuzzyLastNameMatchForEmailSignUp = 69,

        ShippingAddressContainPostalAddress = 70,

        ShippingAddressMatchApplication = 71,

        BillingAddressMatchApplication = 72,

        ShoppingCartMajorInfoUnreliable = 73,

        BankStatementPayDayLoansAndUnemployed = 74,

        BankStatemenNumberOfDishonoursAndUnemployed = 75,

        UnemployedAndInsufficientCapacity = 76,

        VedaConnectBankruptcyNotDischargedNotCompleted = 77,

        CreditSenseNumberOfDishonours = 78,

        CreditSenseNumberOfDishonoursAndUnemployed = 79,

        VedaNumberOfNonTelcoDefaults = 80,

        VedaScoreAndUnemployed = 81,

        CheckHomeOwner = 82,

        CreditSenseNumberOfPayDayLoansAndNotHomeOwner = 83,

        VedaScoreAndNotHomeOwner = 84,

        Defaults24UnpaidGreaterThan = 85,

        Defaults24UnpaidAndNumberOfPayDayLoansGreaterThan = 86,

        DefaultsUnpaidGreaterThan = 87,

        DuplicateFullNameAndDOB = 88,

        DuplicateFullNameAndAddress = 89,

        MaximumAccountsPerBillingAddress = 90,

        PayPalCreditLimitForAccountNotVerified = 91,

        DnbConnectionError = 92,

        ExistingAlternateDeclinedAccountByPayPalMobilePhone = 93,

        ExistingAlternateDeclinedAccountByPayPalLastNameAndDob = 94,

        ExistingAlternateDeclinedAccountByFacebookLastNameAndDob = 95,

        ExistingAlternateDeclinedAccountByDeviceId = 96,

        ExistingAlternateDeclinedAccountBySmartId = 97,

        ExistingAlternateDeclinedAccountByFullNameAndDob = 98,

        ExistingAlternateDeclinedAccountByFullNameAndHomeAddress = 99,

        ExistingAlternateDeclinedAccountByFullNameAndShippingAddress = 100,

        VedaCheckEmployment = 101,

        DnbCheckEmployment = 102,

        VedaIdMatrixConnectionError = 103,

        DecisioningMicroServiceCheckResult = 104,

        DecisioningMicroServiceConnectionError = 105,

        VedaDefaultsLongerThan36Months = 106,

        VedaPayDayMatchAboveThreshold = 107,

        VedaTotalValueOfOutstandingDefaultsGreaterThan = 108,

        DnbTotalValueOfOutstandingDefaultsGreaterThan = 109,

        VedaCreditEnquiries1Month = 110,

        DnbCreditEnquiries1Month = 111,

        DnbNewCreditFile = 112,

        ReOpenClosedAccount = 113,

        PaymentMethodInsufficientFundsOnCardPreAuth = 114,

        PaymentMethodAttemptToAddFraudCard = 115,

        PaymentMethodFailedAttemptsToAddCard = 116,

        PaymentMethodAttemptsToAddExistingCards = 117,

        PaymentMethodAttemptsToAddNonDomesticCards = 118,

        ScriptingRule = 119,

        CreditSenseFullNameAddressMatch = 120,

        CreditSenseCheckTransactionHistory = 121,

        CreditSenseCheckCreditTransaction = 122,

        DecisioningMicroServiceCheckScore = 123,

        RedecisionDueToConnectionError = 124,

        ToBeRedecisioned = 125,

        ApplicationCheckEmailFraudScore = 126,

        FacebookCheckEmailFraudScore = 127,

        PaypalCheckEmailFraudScore = 128,

        AuthoriseRequestCheckEmailFraudScore = 129,

        CreditProfileStateModifierApplied = 130,

        VedaCartShippingAddressMatchLongTermAddress = 131,

        FraudDetectionCheckBlacklist = 132,

        FacebookRemovePositiveScoreIfFullNameNotFuzzyMatch = 133,

        PayPalRemovePositiveScoreIfFullNameNotFuzzyMatch = 134,

        ScoreCardAppliedCreditLimit = 135,

        ApplicationCheckBillingAddressGeoRiskScore = 136,

        FullNameAndBankDetailsExist = 137,

        VedaResponseDoesNotContainNode = 138,

        FraudDetectionPostBureauCheckBlacklist = 139,

        AuthorizeRequestAccountCreatedOnFromMerchant = 140,

        AuthoriseRequestHasPreviousPurchasesFromMerchant = 141,

        AuthoriseRequestFraudCheckResultFromMerchant = 142,

        LinkedInFuzzyNameMatch = 143,

        LinkedInNumberOfConnectionsBelowThreshold = 144,

        LinkedInEmailAddressMatch = 145,

        LinkedInInLocationCountry = 146,

        LinkedInEmploymentStability = 147,

        LinkedInRemovePositiveScoreIfFullNameNotFuzzyMatch = 148,

        LinkedInHasProfilePicture = 149,

        VedaEmploymentMatchLinkedIn = 150,

        GreenIdOverallOutcomeCheck = 151,

        DnbScore = 152,

        CreditSenseEmailMatch = 153,

        CreditSensePhoneMatch = 154,

        CreditSenseFullNameMatch = 155,

        CreditSenseAddressMatch = 156,

        CreditSenseMinimumAccountOpenDate = 157,

        CreditSenseRemovePositiveScoreIfAllRulesFail = 158,

        PayDayMatchCount = 159,

        IdentityCheckInsufficient = 160,

        CreditSenseCombineIdentityScore = 161,

        FacebookCombineIdentityScore = 162,

        PaypalCombineIdentityScore = 163,

        LinkedInCombineIdentityScore = 164,

        AuthoriseRequestCombineIdentityScore = 165,

        DnbScoreAndAgeOfFile = 166,

        DecisionMetricsCheckThreshold = 167,

        SocialDataExists = 168,

        VedaBanPeriod = 169,

        NameIsSameCase = 170,

        CombineRiskRule = 171,

        CombineIdentityRule = 172,

        AccountMigration = 173,

        Application = 174,

        AuthoriseRequest = 175,

        CombineRule = 176,

        CreditSense = 177,

        DecisioningMicroService = 178,

        Device = 179,

        Dnb = 180,

        Duplicate = 181,

        Facebook = 182,

        FraudDetection = 183,

        GreenId = 184,

        LinkedIn = 185,

        PaymentMethod = 186,

        Paypal = 187,

        ThreatMetrix = 188,

        VedaConnect = 189,

        VedaIdMatrix = 190,

        DuplicatePostBureau = 191
    }
}
