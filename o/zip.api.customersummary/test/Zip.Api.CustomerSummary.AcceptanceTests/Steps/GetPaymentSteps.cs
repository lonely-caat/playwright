using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Zip.Api.CustomerSummary.Api;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Steps
{
    [Binding]
    public class GetPaymentSteps : BaseStep
    {
        public GetPaymentSteps(TestWebApplicationFactory<TestStartup> factory) : base(factory)
        {
        }

        [When(@"I make a get request to /payments/id with following test cases")]
        public async Task WhenIMakeAGetRequestToPaymentsIdWithFollowingTestCases(Table table)
        {
            foreach(var testItem in table.Rows)
            {
                var paymentId = testItem["id"];

                response = await _httpClient.GetAsync($"/api/payments/{paymentId}");

                Compare(testItem);
            }
        }
    }
}
