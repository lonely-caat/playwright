namespace Zip.Api.CustomerSummary.Infrastructure.Configuration.OutgoingMessages
{
    public class OutgoingMessagesConfig : IOutgoingMessagesConfig
    {
        public string ZipUrlShortenerJumpPage { get; set; }
        public string ZipUrlShortenerApi { get; set; }
        public string ZipUrlShortenerTitle { get; set; }
        public string ZipUrlShortenerDescription { get; set; }
        public string ZipPayNowBaseUrl { get; set; }
        public string ZipPayNowLinkServiceUrl { get; set; }
        public bool NewPayNowGenerator { get; set; }
    }
}