using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Zip.Api.CustomerSummary.Infrastructure.Configuration
{
    [ExcludeFromCodeCoverage]
    public class CoreApiProxyOptions
    {
        [Required]
        public string BaseUrl { get; set; }
        
        [Required]
        public string ClientId { get; set; }
        
        [Required]
        public string ClientSecret { get; set; }
    }
}