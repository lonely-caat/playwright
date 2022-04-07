using System;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Zip.Api.CustomerSummary.Api;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Steps
{
    [Binding]
    public class GetDefaultPaymentMethodSteps : BaseStep
    {
        public GetDefaultPaymentMethodSteps(TestWebApplicationFactory<TestStartup> factory) : base(factory)
        {
        }

        [When(@"I make a get request to /paymentmethods/default with following test cases")]
        public async Task WhenIMakeAGetRequestToPaymentmethodsDefaultWithFollowingTestCases(Table table)
        {
            foreach(var testItem in table.Rows)
            {
                var consumerId = long.Parse(testItem["consumerId"]);
                response = await _httpClient.GetAsync($"/api/paymentmethods/default?consumerId={consumerId}");

                Assert.Equal(Enum.Parse<HttpStatusCode>(testItem["expected"]), response.StatusCode);
            }
        }
    }
}
