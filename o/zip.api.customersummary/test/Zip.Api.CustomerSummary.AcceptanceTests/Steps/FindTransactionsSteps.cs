using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Zip.Api.CustomerSummary.Api;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Steps
{
    [Binding]
    public class FindTransactionsSteps : BaseStep
    {
        public FindTransactionsSteps(TestWebApplicationFactory<TestStartup> factory) : base(factory)
        {
        }

        [When(@"I make a get request to /transactions with following test cases")]
        public async Task WhenIMakeAGetRequestToTransactionsWithFollowingTestCases(Table table)
        {
            foreach(var testItem in table.Rows)
            {
                var consumerId = long.Parse(testItem["consumerId"]);
                var startDate = DateTime.TryParse(testItem["startDate"], out var sd) ? sd : null as DateTime?;
                var endDate = DateTime.TryParse(testItem["endDate"], out var ed) ? ed : null as DateTime?;

                var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
                queryString.Add("consumerId", $"{consumerId}");

                if (startDate.HasValue)
                {
                    queryString.Add("startDate", startDate.Value.ToLongDateString());
                }

                if(endDate.HasValue)
                {
                    queryString.Add("endDate", endDate.Value.ToLongDateString());
                }

                response = await _httpClient.GetAsync($"/api/transactions?{queryString}");

                Compare(testItem);
            }
        }
    }
}
