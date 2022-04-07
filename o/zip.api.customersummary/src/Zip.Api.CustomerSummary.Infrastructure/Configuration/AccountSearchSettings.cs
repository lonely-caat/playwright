using System;

namespace Zip.Api.CustomerSummary.Infrastructure.Configuration
{
    public class AccountSearchSettings
    {
        public Uri BaseUrl { get; set; }

        public Uri AccountSearchUrl { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
