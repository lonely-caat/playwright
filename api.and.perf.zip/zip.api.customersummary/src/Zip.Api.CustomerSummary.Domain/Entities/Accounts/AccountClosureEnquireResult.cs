namespace Zip.Api.CustomerSummary.Domain.Entities.Accounts
{
    public enum AccountClosureEnquireResult
    {
        PendingTransactions = 0,

        BalancePayout = 1,

        GoodForClose = 2,

        Error = 3,

        TransactionsHaveFutureClearingDays = 4
    }
}
