using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Zip.Api.CustomerSummary.Domain.Entities.Dto.OrderActivity
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OperationRequestType
    {
        Authorise = 1,
        Capture = 2,
        Cancel = 3,
        Refund = 4,
        Quote = 5,
        Purchase = 6,
        Checkout = 7,
        Charge = 8,
        BillPayment = 9,
        ShopAnywhere = 10
    }
}
