namespace Zip.Api.CustomerSummary.Domain.Entities.Tango
{
    public static class LoanMgt
    {
        // API Queries
        public const string ClientsController = "clients";
        public const string AccountsController = "accounts";
        public const string MerchantAccountsController = "ivaccounts";
        public const string AccountControllerQuery = "accounts/";
        public const string AccountArrearsControllerQuery = "accountarrears";
        public const string AccountPayoutQuoteController = "payoutquote/";
        public const string DaysInArrearsFromQueryParam = "DaysInArrearsAsAt_FROM";
        public const string DaysInArrearsToQueryParam = "DaysInArrearsAsAt_TO";
        public const string ArrearsBalanceFromQueryParam = "ArrearsBalanceAsAt_FROM";
        public const string ArrearsBalanceToQueryParam = "ArrearsBalanceAsAt_TO";
        public const string BusinessCodeQueryParam = "BusinessCode";
        public const string PendingAccountSuffix = "_1";
        public const string MerchantPendingAccountSuffix = "_U";
        public const string ClientQueryString = "?clienthash=";
        public const string MerchantCreateAccountQueryString = "?clienthash={0}&ActivateAccount=false";
        public const string TransactionsController = "transactions";
        public const string MerchantTransactionsController = "ivtransactions";
        public const string RepaymentScheduleController = "variations";
        public const string TransactionsFromQuery = "?AccountHash={0}&TransactionDate_FROM={1:yyyy-MM-dd}";
        public const string TransactionsByTypeFromQuery = "?AccountHash={0}&TransactionType={1}&TransactionDate_FROM={2:yyyy-MM-dd}";
        public const string TransactionsForConsumerQuery = "?AccountHash={0}&TransactionType={1}&Reference={2}";
        public const string TransactionsByTypeForConsumerQuery = "?AccountHash={0}&TransactionType={1}";
        public const string TransactionsByType2ForConsumerQuery = "?AccountHash={0}&TransactionType2={1}";
        public const string AccountHashQuery = "?AccountHash=";
        public const string DateQueryFirst = "?AsAtDate=";
        public const string DateQuery = "&AsAtDate=";
        public const string QueryDateFormat = "{0:yyyy-MM-dd}";
        public const string CreateClientOperation = "CreateClient";
        public const string CreateAccountOperation = "CreateAccount";
        public const string CreateMerchantClientOperation = "CreateMerchantClient";
        public const string CreateMerchantAccountOperation = "CreateMerchantAccount";
        public const string CreateTransactionOperation = "CreateTransaction";
        public const string CreateMerchantTransactionOperation = "CreateMerchantTransaction";
        public const string VoidTransactionOperation = "VoidTransaction";
        public const string VoidMerchantTransactionOperation = "VoidMerchantTransaction";
        public const string UpdateAccountOperation = "UpdateAccount";
        public const string ActivateAccountOperation = "ActivateAccount";
        public const string GetAccountOperation = "GetAccount";
        public const string GetMerchantAccountOperation = "GetMerchantAccount";
        public const string GetTransactionsByDateOperation = "GetTransactionsByDate";
        public const string GetTransactionsForConsumerByReference = "GetTransactionsForConsumerByReference";
        public const string GetAccountWriteOffs = "GetAccountWriteOffs";
        public const string GetTransactionsOperation = "GetTransactions";
        public const string GetRepaymentSchedule = "GetRepaymentSchedule";
        public const string ListRepaymentSchedules = "ListRepaymentSchedules";
        public const string AddRepaymentSchedule = "AddRepaymentSchedule";
        public const string UpdateRepaymentSchedule = "UpdateRepaymentSchedule";
        public const string DeleteRepaymentSchedule = "DeleteRepaymentSchedule";

        public const string EndOfMonthStartFormat = "MonthEnd/Start?AsAtDate={0:yyyy-MM-dd}";
        public const string EndOfMonthProcessAccountFormat = "MonthEnd/Process/{0}?AsAtDate={1:yyyy-MM-dd}&MasterBatchNumber={2}";
        public const string EndOfMonthEndFormat = "MonthEnd/End?AsAtDate={0:yyyy-MM-dd}";

        public const string MerchantTransactionQuery = "?AccountHash={0}";
        public const string MerchantTransactionFromDateQuery = "?AccountHash={0}&TransactionDate_FROM={1:yyyy-MM-dd}";
        public const string MerchantTransactionQueryTransactionCode = "{0}&TransactionCode={1}";
        public const string MerchantTransactionQueryFromDate = "{0}&TransactionDate_FROM={1:yyyy-MM-dd}";
        public const string MerchantTransactionQueryToDate = "{0}&TransactionDate_TO={1:yyyy-MM-dd}";
        public const string MerchantTransactionParameterAccountHash = "AccountHash";
        public const string MerchantTransactionParameterTransactionCode = "TransactionCode";
        public const string MerchantTransactionParameterReference = "Reference";
        public const string MerchantTransactionParameterTransactionDateFrom = "TransactionDate_FROM";
        public const string MerchantTransactionParameterTransactionDateTo = "TransactionDate_TO";
        public const string MerchantTransactionParameterPaidByTransactionId = "PaidByTransactionID";
        public const string MerchantTransactionDateFormat = "{0:yyyy-MM-dd}";
        public const string UpdateClientOperation = "UpdateClient";
        public const string GetClientOperation = "GetClient";
        public const string GetMerchantClientOperation = "GetMerchantClient";

        // Charge Codes
        public const string ChargeCodeMerchantPending = "ZMMPE";
        public const string ChargeCodeMerchantDisbursement = "ZMMDI";
        public const string ChargeCodeTransactionFee = "ZMTRF";
        public const string ChargeCodeMerchantRefund = "ZMMRE";
        public const string ChargeCodeEstablishmentFee = "ZMEST";
        public const string ChargeCodeMerchantFee = "ZMMF";
        public const string ChargeCodeMonthlyFee = "ZMMSF";
        public const string ChargeCodeMonthlyFee2 = "ZMASK";
        public const string ChargeCodeZipPayMonthlyFee = "ZPMSF";
        public const string ChargeCodeLateFee = "ZMODI";
        public const string ChargeCodeDishonouredFee = "ZMODI";
        public const string ChargeCodeMercantileFee = "ZMAGF";

        // Narratives
        public const string TransactionFeeNarrative = "Transaction fee";
        public const string RefundTransactionFeeNarrative = "Refund Transaction fee";
        public const string EstablishmentFeeNarrative = "Establishment fee";
        public const string ReOpenTransactionNarrative = "Re-Open Balance Trx";
        public const string ClosingTransactionNarrative = "Closing Balance Trx";
        public const string RefundEstablishmentFeeNarrative = "Establishment fee";
        public const string MerchantFeeNarrative = "Merchant fee";
        public const string MerchantTransactionFeeNarrative = "Merchant Transaction fee";
        public const string MerchantAuthorisedNarrative = "Merchant Authorised";
        public const string RefundMerchantFeeNarrative = "Refund Merchant fee";
        public const string RefundMerchantTransactionFeeNarrative = "Refund Merchant Transaction fee";
        public const string MonthlyFeeNarrative = "Monthly Fee";
        public const string RefundMonthlyFeeNarrative = "Refund Monthly fee";
        public const string LateFeeNarrative = "Late fee";
        public const string MercantileFeeNarrative = "Mercantile Fee";
        public const string RefundLateFeeNarrative = "Refund Late fee";
        public const string RefundInterestNarrative = "Refund Interest";
        public const string CreditNoteNarrative = "Account Credit";
        public const string CancelledCreditNoteNarrative = "Cancelled Account Credit";
        public const string RefundNarrative = "Refund";
        public const string DepositNarrative = "Deposit";
        public const string PaymentOnlyNarrative = "Payment";
        public const string PaymentNarrative = "Direct Debit Payment";
        public const string PaymentRefundNarrative = "Repayment Refund";
        public const string PaymentBPayNarrative = "BPAY Payment";
        public const string PaymentExternalCollectionNarrative = "Externally Collected Payment";
        public const string PaymentDishonourNarrative = "Direct Debit Payment Dishonoured";
        public const string PaymentDishonourFeeNarrative = "Direct Debit Payment Dishonoured Fee";
        public const string RefundDishonourFeeNarrative = "Refund Direct Debit Dishonoured Fee";
        public const string PaymentFailedNarrative = "Direct Debit Declined ${0}";

        // Transaction Types
        public const string DebitTransactionType = "D";
        public const string CreditTransactionType = "C";
        public const string PaymentTransactionType = "P";
        public const string DrawdownTransactionType = "R";
        public const string CloseTransactionType = "O";
        public const string InterestTransactionType = "I";
        public const string ReversalTransactionType = "Z";
        public const string FeeTransactionType2 = "D";
        public const string MerchantFeeCode = "MF";
        public const int MerchantFeeTransactionType = 74;
        public const string MerchantTransactionFeeCode = "MS";
        public const int MerchantTransactionFeeTransactionType = 77;
        public const string MerchantDisbursementCode = "MP";
        public const int MerchantDisbursementTransactionType = 73;
        public const string MerchantRefundCode = "MR";
        public const int MerchantRefundTransactionType = 76;
        public const string MerchantDebitCode = "MD";
        public const int MerchantDebitTransactionType = 75;
        public const int MerchantVoidTransactionType = 81;
        public const string VoidTransaction = "V";
        public const string VoidMerchantTransaction = "VD";
        public const string WriteOffTransactionType2 = "L";

        // Misc
        public const short StdInterestFreePeriod = 3;
        public const short StdPromotionalRate = 0;
        public const string InterestFreeInterval = "Months";
        public const string RiskGrade = "A1000";
        public const int RiskRating = 99;
        public const string SettlementStatus = "N";
        public const int Zero = 0;
        public const decimal MinimumArrearsBalance = 10m;
        public const int OneTerm = 1;
        public const string LoanStatus = "A";
        public const int ClientType = 1;//1 = Person, 2 = Organisation
        public const int ClientMerchantType = 2;//1 = Person, 2 = Organisation

        public const string NewAccountStatus = "New Account";
        public const string OperationalStatus = "Operational";
        public const string NoFurtherDrawDownStatus = "No Further Drawdown";
        public const string LossRecoveryStatus = "Loss Recovery";

        // mike greer - 5 Oct 2017 - All new write offs will now have a GeneralPurpose1 of "Recovery Ledger" or "Loss Recovery" and AccountStatus of ChargedOff
        public const string LostStatus2 = "Recovery Ledger";

        public const string StopCreditStatus = "Stop Credit";
        public const string LostStatus = "Lost";
        public const string ClosedStatus = "Closed";

        public const decimal ClosingBalanceThreshold = 5m;
        public const decimal TransactionAmountZero = 0m;
        public const string MerchantBusinessCode = "MERCHANT";

        // References
        public const string StatementDirectDebit = "Direct Debit Payment";
        public const string StatementInterestCharged = "Interest Charged";
        public const string CreditCardReference = "CredCard";
        public const string DirectDebitReference = "DCT DBT";
        public const string BpayReference = "BPAY";
        public const string PaymentTransactionReference = "P{0}";
        public const string NewPaymentServicePaymentTransactionReference = "PS{0}";
        public const string PaymentRefundTransactionReference = "R{0}";
        public const string NewPaymentServicePaymentRefundTransactionReference = "RPS{0}";
        public const string CloseAccountRef = "Acc {0} Closed";
        public const string NewPaymentServicePaymentDishonouredTransactionReference = "DPS{0}";
    }
}
