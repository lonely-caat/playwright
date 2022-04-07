namespace Zip.Api.CustomerSummary.Infrastructure.Configuration
{
    public class CommunicationsServiceProxyOptions
    {
        public bool Enabled { get; set; } = false;
        public string BaseUrl { get; set; }
    }
}
