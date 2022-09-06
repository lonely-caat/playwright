namespace Zip.Api.CustomerSummary.Domain.Common.Constants
{
    public static class Payments
    {
        public const string BankDetailsNull = "Bank details not found, Account Id:{0}";
        public const string CardNull = "Card details not found, Account Id:{0}";
        public const string AccountNull = "Account not found, Account Id:{0}";
        public const string ConsumerAccountNull = "Consumer Account not found, Account Id:{0}";

        public const string IncorrectRepaymentType =
            "Account Id:{0} is not set for repayment method by '{1}'. It is set to '{2}'";

        public const string ReferenceNotSupplied = "Reference supplied for Account Id:{0} BPAY payment is empty";
        public const string ReferenceTemplate = "zipMoney-Ac{0}-{1}";
        public const string TransactionReferenceTemplate = "zipMoney-{0}-{1}-{2}";
        public const string DescriptionTemplate = "zip-{0}-{1:ddMMyy}";
        public const string TransactionReferenceTypeDeposit = "dep";
        public const string TransactionReferenceTypeAuthorisation = "auth";

        public const string PaymentExchangeFailure =
            "The payment exchange for Account Id:{0} failed. Review the log details";

        public const char CardNumberMask = '*';

        public const string NotificationExpiredCard = "Expired Card";
        public const string NotificationReferToCardIssuer = "Refer to Card Issuer";
        public const string NotificationInsufficientFunds = "Insufficient funds";

        public const string DuplicateCardInProduct = "Card details found in the same product: {0}, please add another card";

        public const string GenericFailure =
            "<p>Just letting you know that today's repayment was unsuccessful.</p><p> Can you please check the default payment method in your Digital Wallet is correct.</p><p>You can sign in to your <a href= \"{0}\"> Digital Wallet</a> now to make or re-schedule a payment, or get in touch to let us know when we should re-process this payment.</p>";

        public const string BankIncorrectDetails =
            "<p>Just letting you know your direct debit payment has been dishonoured as the BSB and Account Number provided were not correct.</p><p>Please sign in to your<a href= \"{0}\"> Digital Wallet</a> to securely update these details (and/or make payment if required).</p><p>Please also let us know when we should re-process this payment.</p>";

        public const string BankInsufficientFunds =
            "<p>Just letting you know your direct debit payment has been dishonoured due to insufficient funds.</p><p>Please sign in to your<a href= \"{0}\"> Digital Wallet</a> now to make a payment or re-schedule.</p><p>Alternatively, please contact us to let us know when we should re-process this payment.</p>";

        public const string CardInsufficientFunds =
            "<p>Just letting you know there were insufficient funds for today's repayment.</p><p>Please sign in to your<a href= \"{0}\"> Digital Wallet</a> now to make a payment or re-schedule.</p><p>Alternatively, you can contact us to let us know when we should re-process this payment.</p>";

        public const string CardDeclined =
            "<p>Just letting you know we were unable to process today's zipMoney repayment.</p><p> Changed your card recently or had any recent problems with your debit card?</p><p>Please sign in to your<a href= \"{0}\"> Digital Wallet</a> now to make or re-schedule a payment, or update your card details.</p><p>Alternatively, you can contact us to let us know when we should re-process this payment.</p>";

        public const string CardExpired =
            "<p>Just letting you know that the card on file for your direct debit payments has now expired.</p><p> Please sign in to your<a href= \"{0}\"> Digital Wallet</a> to securely update these details (and/or make payment if required)</p><p>Please let us know if you have any queries at any stage.</p>";
    }
}
