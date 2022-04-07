using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Zip.Api.CustomerSummary.Domain.Entities.Dto.OrderActivity
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OperationRequestStatus
    {
        New = 1,
        Processed = 2,
        Cancelled = 3,
        Pending = 4,
        Declined = 5,
        Expired = 6,
        Approved = 7,
        Completed = 8,
        Failed = 9
    }
}