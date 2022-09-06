using System;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Zip.Api.CustomerSummary.Api;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Steps
{
    [Binding]
    public class GetPaymentMethodByIdSteps : BaseStep
    {
        public GetPaymentMethodByIdSteps(TestWebApplicationFactory<TestStartup> factory) : base(factory)
        {
        }

        [When(@"I make a get request to /paymentmethods/id with following test cases")]
        public async Task WhenIMakeAGetRequestToPaymentmethodsIdWithFollowingTestCases(Table table)
        {
            foreach(var testItem in table.Rows)
            {
                var id = Guid.TryParse(testItem["id"], out var rid) ? rid : Guid.Empty;

                response = await _httpClient.GetAsync($"/api/paymentmethods/{id}");

                Assert.Equal(Enum.Parse<HttpStatusCode>(testItem["expected"]), response.StatusCode);
            }
        }
    }
}
