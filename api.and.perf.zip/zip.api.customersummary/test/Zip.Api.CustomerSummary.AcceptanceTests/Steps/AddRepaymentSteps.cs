using System;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Zip.Api.CustomerSummary.Api;
using Zip.Api.CustomerSummary.Domain.Entities.Payments;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Steps
{
    [Binding]
    public class AddRepaymentSteps : BaseStep
    {
        public AddRepaymentSteps(TestWebApplicationFactory<TestStartup> factory) : base(factory)
        {
        }

        [When(@"I make a post request to /accounts/accountid/repayment with following repayments to add and expect the response status code is as expected")]
        public async Task WhenIMakeAPostRequestToAccountsAccountidRepaymentWithFollowingRepaymentsToAddAndExpectTheResponseStatusCodeIsAsExpected(Table table)
        {
            foreach (var row in table.Rows)
            {
                var accountId = long.Parse(row["accountId"]);
                var frequency = Enum.Parse<Frequency>(row["frequency"]);
                var startDate = DateTime.Now.Date.AddDays(int.Parse(row["startDate (in * days)"]));
                var amount = decimal.Parse(row["amount"]);
                var changedBy = row["changedBy"];

                var repayment = new Repayment
                {
                    AccountId = accountId,
                    Frequency = frequency,
                    StartDate = startDate,
                    Amount = amount,
                    ChangedBy = changedBy
                };
                
                response = await _httpClient.PostAsync("/api/accounts/repayment", repayment.ToJsonHttpContent());

                var expected = Enum.Parse<HttpStatusCode>(row["statusCode"]);

                Assert.Equal(expected, response.StatusCode);
            }
        }
    }
}
