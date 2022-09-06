namespace Zip.Api.CustomerSummary.Infrastructure.Services.ZipUrlShorteningService.Models
{
    public class ZipUrlShorteningRequest
    {
        public string Action { get; set; }

        public ZipUrlShortenParams Params { get; set; }
    }
}
