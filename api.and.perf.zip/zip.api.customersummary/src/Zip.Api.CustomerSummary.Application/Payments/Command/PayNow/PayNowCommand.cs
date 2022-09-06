using MediatR;

namespace Zip.Api.CustomerSummary.Application.Payments.Command.PayNow
{
    public class PayNowCommand : IRequest
    {
        public long ConsumerId { get; set; }
        public decimal Amount { get; set; }
        public string OriginatorEmail { get; set; }
        public string OriginatorIpAddress { get; set; }
    }
}
