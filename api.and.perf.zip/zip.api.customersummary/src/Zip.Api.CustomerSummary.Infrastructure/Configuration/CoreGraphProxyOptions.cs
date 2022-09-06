using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Zip.Api.CustomerSummary.Infrastructure.Configuration
{
    [ExcludeFromCodeCoverage]
    public class CoreGraphProxyOptions
    {
        [Required]
        public string BaseUrl { get; set; }
    }
}
