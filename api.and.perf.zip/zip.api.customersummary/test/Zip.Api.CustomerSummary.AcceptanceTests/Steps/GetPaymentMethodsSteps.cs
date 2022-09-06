using System;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Zip.Api.CustomerSummary.Api;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Steps
{
    [Binding]
    public class GetPaymentMethodsSteps : BaseStep
    {
        public GetPaymentMethodsSteps(TestWebApplicationFactory<TestStartup> factory) : base(factory)
        {
        }

        [When(@"I make a get request to /paymentmethods with following test cases")]
        public async Task WhenIMakeAGetRequestToPaymentmethodsWithFollowingTestCases(Table table)
        {
            foreach(var testItem in table.Rows)
            {
                var consumerId = long.Parse(testItem["consumerId"]);
                var state = testItem["state"];
                response = await _httpClient.GetAsync($"/api/paymentmethods?consumerId={consumerId}&state={state}");

                Assert.Equal(Enum.Parse<HttpStatusCode>(testItem["expected"]), response.StatusCode);
            }
        }
    }
}
