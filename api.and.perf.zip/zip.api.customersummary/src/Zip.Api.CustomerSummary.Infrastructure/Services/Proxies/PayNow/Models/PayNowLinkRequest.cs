namespace Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.PayNow.Models
{
    public class PayNowLinkRequest
    {
        public long ConsumerId { get; set; }

        public decimal Amount { get; set; }
    }
}