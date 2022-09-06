using System;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Zip.Api.CustomerSummary.Api;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Steps
{
    [Binding]
    public class RefundSteps : BaseStep
    {
        public RefundSteps(TestWebApplicationFactory<TestStartup> factory) : base(factory)
        {
        }

        [When(@"I make a post request to /payments/id/refund with following test cases")]
        public async Task WhenIMakeAPostRequestToPaymentsIdRefundWithFollowingTestCases(Table table)
        {
            foreach(var testItem in table.Rows)
            {
                var id = Guid.TryParse(testItem["id"], out var i) ? i : Guid.Empty;

                response = await _httpClient.PostAsync($"/api/payments/{id}/refund", new StringContent(""));

                Compare(testItem);
            }
        }
    }
}
