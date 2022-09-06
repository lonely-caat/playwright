namespace Zip.Api.CustomerSummary.Domain.Entities.Accounts
{
    public enum CustomerSearchType
    {
        NoFilter = 0,

        AccountNumber = 1,

        LastName = 2,

        EmailAddress = 3,

        MobileNumber = 5,

        ConsumerId = 7,

        Reference = 8,

        PublicConsumerId = 9,
    }
}