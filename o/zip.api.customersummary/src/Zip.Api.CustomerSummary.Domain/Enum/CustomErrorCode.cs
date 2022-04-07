using System.Runtime.Serialization;

namespace Zip.Api.CustomerSummary.Domain.Enum
{
    public enum CustomErrorCode
    {
        // Generic Error Codes
        [EnumMember(Value = "01")]
        ServerError = 1,

        [EnumMember(Value = "02")]
        AccessDenied = 2,

        [EnumMember(Value = "03")]
        UnauthorizedAccess = 3,

        [EnumMember(Value = "04")]
        ExternalCall = 4,

        [EnumMember(Value = "05")]
        NotFound = 5,

        [EnumMember(Value = "06")]
        InvalidRequest = 6,

        [EnumMember(Value = "07")]
        NoContent = 7,

        [EnumMember(Value = "08")]
        Forbidden = 8,

        [EnumMember(Value = "09")]
        BadRequest = 9,

        // Processing Error Codes
        [EnumMember(Value = "21")]
        InvalidAccountType = 21,

        [EnumMember(Value = "22")]
        InvalidPayload = 22,

        [EnumMember(Value = "23")]
        UnauthorizedAction = 23,

        [EnumMember(Value = "24")]
        EntityFailure = 24,

        [EnumMember(Value = "25")]
        TransactionFailure = 25,

        [EnumMember(Value = "26")]
        ShoppingCartDetailNotFound = 26,

        [EnumMember(Value = "26")]
        InvalidStaffRefCode = 26,

        [EnumMember(Value = "27")]
        ConsumerNotFound = 27,

        [EnumMember(Value = "28")]
        InvalidProduct = 28,

        [EnumMember(Value = "29")]
        InvalidToken = 29,

        [EnumMember(Value = "30")]
        AccountNotFound = 30,

        [EnumMember(Value = "31")]
        NoValidAccountType = 31
    }
}
