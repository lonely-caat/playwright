using System;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Zip.Api.CustomerSummary.Api;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Steps
{
    [Binding]
    public class GetLatestPaymentMethodsSteps : BaseStep
    {
        public GetLatestPaymentMethodsSteps(TestWebApplicationFactory<TestStartup> factory) : base(factory)
        {
        }

        [When(@"I make a get request to /paymentmethods/latest with following test cases")]
        public async Task WhenIMakeAGetRequestToPaymentmethodsLatestWithFollowingTestCases(Table table)
        {
            foreach (var testItem in table.Rows)
            {
                var consumerId = long.Parse(testItem["consumerId"]);
                var state = testItem["state"];
                response = await _httpClient.GetAsync($"/api/paymentmethods/latest?consumerId={consumerId}&state={state}");

                Assert.Equal(Enum.Parse<HttpStatusCode>(testItem["expected"]), response.StatusCode);
            }
        }
    }
}
