using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Zip.Api.CustomerSummary.Api;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Steps
{
    [Binding]
    public class GetAvailableStatementDatesSteps : BaseStep
    {
        public GetAvailableStatementDatesSteps(TestWebApplicationFactory<TestStartup> factory) : base(factory)
        {
        }

        [When(@"I make a get request to /statements/availabledate with following test cases")]
        public async Task WhenIMakeAGetRequestToStatementsAvailabledateWithFollowingTestCases(Table table)
        {
            foreach(var testItem in table.Rows)
            {
                var accountId = long.Parse(testItem["accountId"]);
                response = await _httpClient.GetAsync($"/api/statements/availabledate?accountId={accountId}");

                Compare(testItem);
            }
        }
    }
}
