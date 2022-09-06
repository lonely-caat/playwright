using System;

namespace Zip.Api.CustomerSummary.Domain.Entities.Dto.OrderActivity
{
    public class OrderActivityDto
    {
        public DateTime TimeStamp { get; set; }
        public long Id { get; set; } // OperationRequestId
        public long? ParentOperationRequestId { get; set; }
        public string Reference { get; set; }
        public string MerchantName { get; set; }
        public string ShippingAddress { get; set; }
        public long Amount { get; set; }
        public OperationRequestType Type { get; set; }
        public OperationRequestStatus Status { get; set; }
        public string Metadata { get; set; } // JSON string
    }
}
