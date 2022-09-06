using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Zip.Api.CustomerSummary.Api;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Steps
{
    [Binding]
    public class FindPaymentsSteps : BaseStep
    {
        public FindPaymentsSteps(TestWebApplicationFactory<TestStartup> factory) : base(factory)
        {
        }

        [When(@"I make a get request to /payments with following query strings")]
        public async Task WhenIMakeAGetRequestToPaymentsWithFollowingQueryStrings(Table table)
        {
            foreach (var testItem in table.Rows)
            {
                var fromDate = DateTime.TryParse(testItem["fromDate"], out var fd) ? fd : null as DateTime?;
                var toDate = DateTime.TryParse(testItem["toDate"], out var td) ? td : null as DateTime?;
                var batchId = Guid.TryParse(testItem["batchId"], out var bi) ? bi : null as Guid?;


                var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
                queryString.Add("accountId", testItem["accountId"]);
                if (fromDate.HasValue)
                {
                    queryString.Add("fromDate", fromDate.Value.ToLongDateString());
                }

                if (toDate.HasValue)
                {
                    queryString.Add("toDate", toDate.Value.ToLongDateString());
                }

                if(batchId.HasValue)
                {
                    queryString.Add("paymentBatchId", $"{batchId.Value}");
                }

                response = await _httpClient.GetAsync($"/api/payments?{queryString}");

                Compare(testItem);
            }
        }
    }
}
