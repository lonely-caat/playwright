using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Zip.Api.CustomerSummary.Api;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetRepaymentSchedule.Models;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Steps
{
    [Binding]
    public class GetRepaymentScheduleSteps : BaseStep
    {
        public GetRepaymentScheduleSteps(TestWebApplicationFactory<TestStartup> factory) : base(factory)
        {
        }

        [When(@"I make a get request to /accounts/accountid/repaymentschedule with the following accountIds and the response status code should be match")]
        public async Task WhenIMakeAGetRequestToAccountsAccountidRepaymentscheduleWithTheFollowingAccountIdsAndTheResponseStatusCodeShouldBeMatch(Table table)
        {
            foreach (var row in table.Rows)
            {
                response = await _httpClient.GetAsync($"/api/accounts/{row["accountId"]}/repaymentschedule");
                Enum.TryParse<HttpStatusCode>(row["statusCode"], out var expected);
                Assert.Equal(expected, response.StatusCode);

                if (expected == HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<GetRepaymentScheduleQueryResult>(content);

                    Assert.NotNull(result);
                }
            }
        }
    }
}
