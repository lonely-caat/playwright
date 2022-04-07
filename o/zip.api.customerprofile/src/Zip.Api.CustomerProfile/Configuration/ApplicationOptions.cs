using System.Diagnostics.CodeAnalysis;

namespace Zip.Api.CustomerProfile.Configuration
{
    [ExcludeFromCodeCoverage]
    public class ApplicationOptions
    {
        public int Port { get; set; }
    }
}