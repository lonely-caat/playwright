namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Services.PaymentWebhook
{
    public static class PaymentWebhookServiceTestConstants
    {
        public const string ValidFullTransaction =
            "{\"id\":\"43e7fca7-bf3e-4914-9585-9eba75758fa7\",\"timestamp\":\"2020-11-01T01:27:25Z\",\"reprocessCount\":1,\"externalId\":\"69df288c-7bec-4885-a89b-e910f206db2a\"," +
            "\"currency\":\"AUD\",\"zipBatchId\":1604194096887,\"forcedReprocess\":false,\"declineReason\":null,\"status\":\"BATCH_PROCESSED\",\"authId\":\"863224\",\"stan\":\"268184\"," +
            "\"rrn\":\"030601268184\",\"valueMinor\":2287,\"sourceRequestId\":\"13f5debb-7861-437d-bf19-8b9532f2dc52\",\"eventType\":\"authorization\",\"source\":\"MARQETA\",\"state\":\"PENDING\"," +
            "\"referenceId\":null,\"requestAmount\":22.87,\"marqetaData\":{\"type\":\"AUTHORIZATION\",\"state\":\"PENDING\",\"identifier\":\"1458160\",\"token\":\"43e7fca7-bf3e-4914-9585-9eba75758fa7\"," +
            "\"userToken\":\"a6892b13-0437-40a0-8730-5af5241de553\",\"actingUserToken\":\"a6892b13-0437-40a0-8730-5af5241de553\",\"cardToken\":\"69df288c-7bec-4885-a89b-e910f206db2a\"," +
            "\"precedingRelatedTransactionToken\":null,\"gpa\":{\"currencyCode\":\"AUD\",\"ledgerBalance\":34.82,\"availableBalance\":0,\"creditBalance\":0,\"pendingCredits\":0,\"impactedAmount\":-22.87," +
            "\"balances\":{\"AUD\":{\"currencyCode\":\"AUD\",\"ledgerBalance\":34.82,\"availableBalance\":0.0,\"creditBalance\":0.0,\"pendingCredits\":0.0,\"impactedAmount\":-22.87}}}," +
            "\"createdTime\":\"2020-11-01T01:27:25Z\",\"userTransactionTime\":\"2020-11-01T01:27:25Z\",\"settlementDate\":\"2020-11-01T00:00:00Z\",\"requestAmount\":22.87,\"amount\":22.87," +
            "\"currencyConversion\":{\"network\":{\"originalAmount\":22.87,\"conversionRate\":1,\"originalCurrencyCode\":\"036\"}},\"issuerInterchangeAmount\":null,\"currency\":\"AUD\"," +
            "\"approvalCode\":\"863224\",\"response\":{\"code\":\"0000\",\"memo\":\"Approved or completed successfully\"},\"network\":\"VISA\",\"subnetwork\":\"VISANET\",\"acquirerFeeAmount\":0," +
            "\"acquirer\":{\"institutionCountry\":\"036\",\"institutionIdcode\":\"456445\",\"rrn\":\"030601268184\",\"stan\":\"268184\"},\"digitalWalletToken\":{" +
            "\"token\":\"8dec9c44-9997-4723-bc36-70a077b52e78\",\"cardToken\":\"69df288c-7bec-4885-a89b-e910f206db2a\",\"state\":\"ACTIVE\",\"fulfillmentStatus\":\"PROVISIONED\"," +
            "\"issuerEligibilityDecision\":\"decision due to TSP risk manager\",\"createdTime\":\"2020-10-19T21:51:22Z\",\"lastModifiedTime\":\"2020-10-19T21:51:43Z\"," +
            "\"tokenServiceProvider\":{\"tokenReferenceId\":\"DNITHE462029378669530636\",\"panReferenceId\":\"V-4620293786696988946155\",\"tokenRequestorId\":\"40010075001\"," +
            "\"tokenRequestorName\":\"ANDROID_PAY\",\"tokenType\":\"DEVICE_CLOUD_BASED\",\"tokenScore\":null,\"tokenAssurancelevel\":null,\"tokenEligibilityDecision\":\"DECISION_YELLOW\"," +
            "\"tokenPan\":\"464200______1437\",\"tokenExpiration\":\"1223\"},\"device\":{\"type\":\"MOBILE_PHONE_OR_TABLET\",\"languageCode\":\"eng\",\"deviceId\":\"EcIfPd7uoPjtqOuAT6GB9tb_\"," +
            "\"phoneNumber\":null,\"name\":\"Google - Pixel 2 XL\",\"location\":null,\"ipAddress\":\"120.17.232.200\",\"token\":null},\"walletProviderProfile\":{\"account\":{" +
            "\"id\":\"tZvK6udqAtQYjnpM_YJoK2OX\",\"emailAddress\":\"ED561B76929E4040DB413609889B610FACBD7B5412641FF3E24C98DE519E9EB2\",\"score\":\"4\"},\"riskAssessment\":null," +
            "\"deviceScore\":\"4\",\"panSource\":\"MOBILE_BANKING_APP\",\"reasonCode\":\"AG,AO,A0,A2\"},\"addressVerification\":{\"gateway\":null,\"name\":null,\"streetAddress\":\"808 Park CHIPPENDALE\"," +
            "\"postalCode\":null},\"stateReason\":\"Digital wallet token provisioned to digital wallet\"},\"user\":{\"metadata\":{}},\"card\":{\"metadata\":{},\"lastFour\":\"6522\"}," +
            "\"cardSecurityCodeVerification\":{\"type\":null,\"response\":{\"code\":\"0000\",\"memo\":\"Card security code match\"}},\"fraud\":null,\"cardholderAuthenticationData\":null," +
            "\"issuerReceivedTime\":\"2020-11-01T01:27:25.861Z\",\"issuerPaymentNode\":\"4a73181801ab87b9ce3664da9561f683\",\"networkReferenceId\":\"380306052455433\",\"cardAcceptor\":{" +
            "\"mid\":\"000000000427858\",\"mcc\":\"5411\",\"networkMid\":null,\"mccGroups\":null,\"name\":\"WOOLWORTHS 1905\",\"address\":null,\"streetAddress\":null,\"city\":\"BROADWAY\",\"state\":null," +
            "\"postalCode\":null,\"zip\":null,\"countryCode\":\"AUS\",\"country\":null,\"poi\":null,\"ecommerceSecurityLevelIndicator\":null},\"gpaOrder\":{\"token\":\"1f76d61c-abcb-4948-9928-d8be720139b9\"," +
            "\"amount\":22.87,\"createdTime\":\"2020-11-01T01:27:27Z\",\"lastModifiedTime\":\"2020-11-01T01:27:27Z\",\"transactionToken\":\"61a600e2-8dfc-4c81-8e51-e27b36109815\",\"state\":\"PENDING\"," +
            "\"response\":{\"code\":\"0000\",\"memo\":\"Approved or completed successfully\"},\"funding\":{\"amount\":22.87,\"source\":{\"type\":\"programgateway\",\"token\":\"**********EWAY\",\"active\":true," +
            "\"name\":\"Zip Co JIT Gateway Funding Source\",\"createdTime\":\"2020-09-01T23:04:09Z\",\"lastModifiedTime\":\"2020-09-01T23:04:09Z\",\"defaultAccount\":null},\"gatewayLog\":{\"orderNumber\":" +
            "\"43e7fca7-bf3e-4914-9585-9eba75758fa7\",\"transactionId\":\"13f5debb-7861-437d-bf19-8b9532f2dc52\",\"message\":\"Approved or completed successfully\",\"duration\":1029,\"timedOut\":false," +
            "\"response\":{\"code\":\"200\",\"data\":{\"jitFunding\":{\"token\":\"13f5debb-7861-437d-bf19-8b9532f2dc52\",\"method\":\"PGFS_AUTHORIZATION\",\"userToken\":\"a6892b13-0437-40a0-8730-5af5241de553\"," +
            "\"actingUserToken\":null,\"amount\":22.87,\"addressVerification\":null,\"originalJitFundingToken\":null,\"incrementalAuthorizationJitFundingTokens\":null}}}}}," +
            "\"fundingSourceToken\":\"**********EWAY\",\"jitFunding\":{\"token\":\"13f5debb-7861-437d-bf19-8b9532f2dc52\",\"method\":\"PGFS_AUTHORIZATION\",\"userToken\":\"a6892b13-0437-40a0-8730-5af5241de553\"," +
            "\"actingUserToken\":\"a6892b13-0437-40a0-8730-5af5241de553\",\"amount\":22.87,\"addressVerification\":null,\"originalJitFundingToken\":null,\"incrementalAuthorizationJitFundingTokens\":null}," +
            "\"userToken\":\"a6892b13-0437-40a0-8730-5af5241de553\",\"currencyCode\":\"AUD\",\"tags\":null,\"memo\":null},\"gpaOrderUnload\":null,\"pos\":{\"panEntryMode\":\"CHIP_CONTACTLESS\"," +
            "\"pinentryMode\":\"TRUE\",\"terminalId\":\"W1905073\",\"terminalAttendance\":\"UNSPECIFIED\",\"cardHolderPresence\":true,\"cardPresence\":true,\"partialApprovalcapable\":false," +
            "\"purchaseAmountOnly\":false,\"recurring\":null},\"transactionMetadata\":{\"payment_channel\":\"OTHER\"}},\"digitalWalletToken\":\"8dec9c44-9997-4723-bc36-70a077b52e78\"," +
            "\"networkReferenceId\":\"380306052455433\"}";
        public const string TransactionWithOnlyId = "{\"id\":\"43e7fca7-bf3e-4914-9585-9eba75758fa7\"}";
        public const string TransactionWithOlderTimestamp = "{\"timestamp\":\"2000-01-01T00:00:00Z\"}";
        public const string TransactionWithNewerTimestamp = "{\"timestamp\":\"2002-02-02T00:00:00Z\"}";
    }
}
