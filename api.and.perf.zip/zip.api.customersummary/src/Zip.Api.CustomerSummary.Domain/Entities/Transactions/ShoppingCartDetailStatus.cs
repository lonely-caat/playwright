using System.Runtime.Serialization;

namespace Zip.Api.CustomerSummary.Domain.Entities.Transactions
{
    public enum ShoppingCartDetailStatus
    {
        InProgress = 1,
        UnderReview = 2,
        ContractPending = 3,
        Declined = 4,
        Authorised = 5,
        [EnumMember(Value = "Captured")]
        Completed = 6,
        Refunded = 7,
        Cancelled = 8,
        Removed = 9,
        DepositRequired = 10,
        PartiallyCaptured = 11
    }
}
