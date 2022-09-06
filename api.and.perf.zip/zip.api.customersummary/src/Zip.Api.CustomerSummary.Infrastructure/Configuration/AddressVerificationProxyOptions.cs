using System;

namespace Zip.Api.CustomerSummary.Infrastructure.Configuration
{
    public class AddressVerificationProxyOptions
    {
        public Uri ServiceUrl { get; set; }
        
        public string RequestKey { get; set; }
        
        public bool Enabled { get; set; }
    }
}
