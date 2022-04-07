using System;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Zip.Api.CustomerSummary.Api;
using Zip.Api.CustomerSummary.Application.Accounts.Command.CloseAccount;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Steps
{
    [Binding]
    public class CloseAccountSteps : BaseStep
    {
        public CloseAccountSteps(TestWebApplicationFactory<TestStartup> factory) : base(factory)
        {
        }

        [When(@"I make a post request to /consumers/id/closeaccount with following data")]
        public async Task WhenIMakeAPostRequestToConsumersIdCloseaccountWithFollowingData(Table table)
        {
            foreach (var testItem in table.Rows)
            {
                var consumerId = long.Parse(testItem["consumerId"]);

                var closeAccountCommand = new CloseAccountCommand
                {
                    AccountId = long.Parse(testItem["accountId"]),
                    ConsumerId = consumerId,
                    ChangedBy = testItem["changedBy"],
                    CreditStateType = Enum.TryParse<CreditProfileStateType>(testItem["creditStateType"], out var result) ? result : null as CreditProfileStateType?,
                    CreditProfileId = long.TryParse(testItem["creditProfileId"], out var v) ? v : 0,
                    Comments = testItem["comments"]
                };
                
                response = await _httpClient.PostAsync($"/api/consumers/{consumerId}/closeaccount", closeAccountCommand.ToJsonHttpContent());
                
                Assert.Equal(Enum.Parse<HttpStatusCode>(testItem["expected"]), response.StatusCode);
            }
        }
    }
}
