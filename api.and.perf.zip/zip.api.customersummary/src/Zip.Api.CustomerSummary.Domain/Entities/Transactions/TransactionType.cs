namespace Zip.Api.CustomerSummary.Domain.Entities.Transactions
{
    public enum TransactionType
    {
        Authorised = 1,

        Cancelled = 2,

        Captured = 3,

        Completed = 4,

        Refunded = 5,

        ChargedBack = 6,

        MerchantFee = 7,

        TransactionFee = 8,

        RefundedMerchantFee = 9,

        RefundedTransactionFee = 10,

        Review = 11,

        Declined = 12,

        EstablishmentFee = 13,

        Deposit = 14,

        Payment = 15,

        RefundedEstablishmentFee = 16,

        CreditNote = 17,

        CancelledCreditNote = 18,

        RefundedMonthlyFee = 19,

        RefundedInterest = 20,

        LateFee = 21,

        RefundedLateFee = 22,

        ClosePending = 23,

        CloseActive = 24,

        Interest = 25,

        MonthlyFee = 26,

        VoidCancelled = 27,

        VoidCaptured = 28,

        VoidTransactionFee = 29,

        VoidMerchantFee = 30,

        ApplicationApproved = 31,

        VoidRefunded = 32,

        VoidRefundedMerchantFee = 33,

        RepaymentRefund = 34,

        ReOpenPending = 35,

        ReOpenActive = 36,

        DishonouredFee = 37,

        RefundedDishonouredFee = 38,

        WriteOff = 39,

        CapturedMerchantTransaction = 40,

        VoidMerchantTransactionFee = 41,

        RefundedMerchantTransactionFee = 42,

        VoidRefundedMerchantTransactionFee = 43,

        VoidCapturedMerchantTranscation = 44,

        RefundedMerchantTransaction = 45,

        VoidRefundedMerchantTransaction = 46,

        MerchantTransactionFee = 47,

        MerchantAuthorised = 48,

        VoidMerchantAuthorised = 49,

        MercantileFee = 50,

        UnWriteOff = 51,

        PartPayMonthlyFee = 52,

        RefundedPartPayMonthlyFee = 53,

        ZipBizBasicMonthlyFee = 54,

        RefundedZipBizBasicMonthlyFee = 55,

        BalanceTransferFromInstalments = 56,

        BalanceTransferFromInterestBearing = 57,

        BalanceTransferToInstalments = 58,

        BalanceTransferToInterestBearing = 59,

        ExtensionFee = 60,

        RefundExtensionFee = 61
    }
}
