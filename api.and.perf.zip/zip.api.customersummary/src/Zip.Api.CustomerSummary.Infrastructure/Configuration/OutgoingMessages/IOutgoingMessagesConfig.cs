namespace Zip.Api.CustomerSummary.Infrastructure.Configuration.OutgoingMessages
{
    public interface IOutgoingMessagesConfig
    {
        string ZipUrlShortenerJumpPage { get; }
        string ZipUrlShortenerApi { get; }
        string ZipUrlShortenerTitle { get; }
        string ZipUrlShortenerDescription { get; }
        string ZipPayNowBaseUrl { get; }
        string ZipPayNowLinkServiceUrl { get; }
        bool NewPayNowGenerator {get; }
    }
}