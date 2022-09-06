namespace Zip.Api.CustomerSummary.Domain.Entities.MessageLog
{
    public enum MessageLogCategory : short
    {
        Application = 0,

        Consumer = 1,

        Merchant = 2,

        OperationRequest = 3,

        System = 4,

        MerchantBranch = 5
    }
}
