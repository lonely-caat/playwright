using System;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Zip.Api.CustomerSummary.Api;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Steps
{
    [Binding]
    public class GetConsumerSteps : BaseStep
    {
        public GetConsumerSteps(TestWebApplicationFactory<TestStartup> factory) : base(factory)
        {
        }

        [Given(@"I have following ConsumerId")]
        public void GivenIHaveFollowingConsumerId()
        {
        }
        
        [When(@"I make a get request to /consumers/id")]
        public async Task WhenIMakeAGetRequestToConsumersId(Table table)
        {
            foreach(var testItem in table.Rows)
            {
                var consumerId = int.Parse(testItem[0]);
                var expected = Enum.Parse<HttpStatusCode>(testItem[1]);

                response = await _httpClient.GetAsync($"/api/consumers/{consumerId}");

                Assert.Equal(expected, response.StatusCode);
            }
        }
        
        [Then(@"the response status code should be as expected")]
        public void ThenTheResponseStatusCodeShouldBeAsExpected()
        {
        }
    }
}
