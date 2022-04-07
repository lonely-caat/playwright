using System.ComponentModel.DataAnnotations;

namespace Zip.Api.CustomerSummary.Infrastructure.Configuration
{
    public class CustomerCoreApiProxyOptions
    {
        [Required]
        public string BaseUrl { get; set; }
    }
}
