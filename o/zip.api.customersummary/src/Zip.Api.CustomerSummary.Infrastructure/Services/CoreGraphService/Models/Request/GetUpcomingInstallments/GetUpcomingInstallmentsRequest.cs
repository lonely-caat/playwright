using System;
using Newtonsoft.Json;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Request.GetUpcomingInstallments
{
    public class GetUpcomingInstallmentsRequest
    {
        public GetUpcomingInstallmentsRequest(
            string query,
            long accountId,
            Guid customerId,
            DateTime toDate)
        {
            Query = query;
            Variables = new GetUpcomingInstallmentsRequestVariables(accountId, customerId, toDate);
        }
        
        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("variables")]
        public GetUpcomingInstallmentsRequestVariables Variables { get; set; }
    }
}
