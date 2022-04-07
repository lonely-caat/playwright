using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Zip.Api.CustomerSummary.Api;
using Zip.Api.CustomerSummary.Domain.Entities.Transactions;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Steps
{
    [Binding]
    public class GetLmsTransactionsSteps : BaseStep
    {
        public GetLmsTransactionsSteps(TestWebApplicationFactory<TestStartup> factory) : base(factory)
        {
        }

        [When(@"I make a get request to /accounts/accountId/lmstransactions with the following account ids the response status code should be expected")]
        public async Task WhenIMakeAGetRequestToAccountsLmstransactionsWithTheFollowingAccountIdsTheResponseStatusCodeShouldBeExpected(Table table)
        {
            foreach(var row in table.Rows)
            {
                response = await _httpClient.GetAsync($"/api/accounts/{row["accountId"]}/lmstransactions");
                Enum.TryParse<HttpStatusCode>(row["statusCode"], out var expected);
                Assert.Equal(expected, response.StatusCode);

                if (expected == HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var trans = JsonConvert.DeserializeObject<IEnumerable<LmsTransaction>>(content);

                    foreach (var tr in trans)
                    {
                        Assert.IsType<LmsTransaction>(tr);
                    }
                }
            }
        }
    }
}
