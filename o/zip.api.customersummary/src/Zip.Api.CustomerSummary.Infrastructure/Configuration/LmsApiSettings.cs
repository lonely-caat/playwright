using System;

namespace Zip.Api.CustomerSummary.Infrastructure.Configuration
{
    public class LmsApiSettings
    {
        public Uri BaseUrl { get; set; }
        public string GetAccountEndpoint { get; set; }
    }
}
