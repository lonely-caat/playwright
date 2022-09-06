using System.Diagnostics.CodeAnalysis;

namespace Zip.Api.CustomerSummary.Infrastructure.Configuration
{
    [ExcludeFromCodeCoverage]
    public class MfaApiProxyOptions
    {
        public string BaseUrl { get; set; }
    }
}