using System;
using Newtonsoft.Json;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Request.GetUpcomingInstallments
{
    public class GetUpcomingInstallmentsRequestVariables
    {
        public GetUpcomingInstallmentsRequestVariables(
            long accountId,
            Guid customerId,
            DateTime toDate)
        {
            AccountV1Input = new GetUpcomingInstallmentsAccountInput(accountId, customerId);
            UpcomingInstallmentsInput = new GetUpcomingInstallmentsInput(toDate);
        }

        [JsonProperty("accountV1Input")]
        public GetUpcomingInstallmentsAccountInput AccountV1Input { get; set; }

        [JsonProperty("upcomingInstallmentsInput")]
        public GetUpcomingInstallmentsInput UpcomingInstallmentsInput { get; set; }
    }
}
