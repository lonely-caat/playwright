using System;
using Newtonsoft.Json;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Request.GetUpcomingInstallments
{
    public class GetUpcomingInstallmentsAccountInput
    {
        public GetUpcomingInstallmentsAccountInput(
            long accountId,
            Guid customerId)
        {
            AccountId = accountId;
            CustomerId = customerId;
        }

        [JsonProperty("accountId")]
        public long AccountId { get; set; }

        [JsonProperty("customerId")]
        public Guid CustomerId { get; set; }
    }
}
