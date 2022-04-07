using System;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Zip.Api.CustomerSummary.Api;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Steps
{
    [Binding]
    public class GetCrmCommunicationsSteps : BaseStep
    {
        public GetCrmCommunicationsSteps(TestWebApplicationFactory<TestStartup> factory) : base(factory)
        {
        }

        [When(@"I make a get request to /crmcommunications with following test cases")]
        public async Task When_Make_GetRequest_To_CrmCommunications_With_Following_TestCases(Table table)
        {
            foreach (var testItem in table.Rows)
            {
                var consumerId = long.Parse(testItem["consumerId"]);
                
                response = await _httpClient.GetAsync($"/api/crmcommunications/{consumerId}");
                
                Assert.Equal(Enum.Parse<HttpStatusCode>(testItem["expected"]), response.StatusCode);
            }
        }
    }
}
