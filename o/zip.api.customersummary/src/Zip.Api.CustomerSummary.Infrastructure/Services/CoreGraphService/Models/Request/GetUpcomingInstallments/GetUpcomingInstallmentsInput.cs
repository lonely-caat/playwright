using System;
using Newtonsoft.Json;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Request.GetUpcomingInstallments
{
    public class GetUpcomingInstallmentsInput
    {
        public GetUpcomingInstallmentsInput(DateTime toDate)
        {
            ToDate = toDate;
        }
        
        [JsonProperty("toDate")]
        public DateTime ToDate { get; set; }
    }
}
