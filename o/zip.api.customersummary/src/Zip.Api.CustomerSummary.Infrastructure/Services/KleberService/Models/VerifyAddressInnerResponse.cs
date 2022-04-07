using System.Collections.Generic;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.KleberService.Models
{
    public class VerifyAddressInnerResponse
    {
        public string RequestId { get; set; }
        
        public string ResultCount { get; set; }
        
        public string ErrorMessage { get; set; }
        
        public IEnumerable<VerifyAddressResponseResult> Result { get; set; }
    }
}