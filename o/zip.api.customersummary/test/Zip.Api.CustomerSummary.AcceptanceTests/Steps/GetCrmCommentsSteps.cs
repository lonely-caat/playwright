using System;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Zip.Api.CustomerSummary.Api;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Steps
{
    [Binding]
    public class GetCrmCommentsSteps : BaseStep
    {
        public GetCrmCommentsSteps(TestWebApplicationFactory<TestStartup> factory) : base(factory)
        {
        }

        [When(@"I make a get request to /crmcomments with following test cases")]
        public async Task WhenIMakeAGetRequestToCrmcommentsWithFollowingTestCases(Table table)
        {
            foreach(var testItem in table.Rows)
            {
                var consumerId = long.Parse(testItem["consumerId"]);
                response = await _httpClient.GetAsync($"/api/crmcomments/{consumerId}");

                Assert.Equal(Enum.Parse<HttpStatusCode>(testItem["expected"]), response.StatusCode);
            }
        }
    }
}
