namespace Zip.Api.CustomerSummary.Infrastructure.Services.PayNowUrlGenerator.Models
{
    public class GeneratePayNowUrlRequest
    {
        public long ConsumerId { get; set; }

        public decimal Amount { get; set; }
    }
}
