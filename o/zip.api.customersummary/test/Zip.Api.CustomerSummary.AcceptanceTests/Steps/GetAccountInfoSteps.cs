using System;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Zip.Api.CustomerSummary.Api;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Steps
{
    [Binding]
    public class GetAccountInfoSteps : BaseStep
    {
        public GetAccountInfoSteps(TestWebApplicationFactory<TestStartup> factory) : base(factory)
        {
        }

        [When(@"I make get request to /consumers/accountinfo with following data")]
        public async Task WhenIMakeGetRequestToConsumersAccountinfoWithFollowingData(Table table)
        {
            foreach(var testItem in table.Rows)
            {
                var consumerId = long.Parse(testItem["consumerId"]);
                response = await _httpClient.GetAsync($"/api/consumers/{consumerId}/account");

                var expected = Enum.Parse<HttpStatusCode>(testItem["expected"]);
                Assert.Equal(expected, response.StatusCode);
            }
        }
        
        [Then(@"the response status code should as expected")]
        public void ThenTheResponseStatusCodeShouldAsExpected()
        {
        }
    }
}
