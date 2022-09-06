using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Zip.Api.CustomerSummary.Infrastructure.Configuration
{
    [ExcludeFromCodeCoverage]
    public class MerchantDashBoardApiOptions
    {
        [Required]
        public string BaseUrl { get; set; }
        
        [Required]
        public string ApiKey { get; set; }
    }
}
