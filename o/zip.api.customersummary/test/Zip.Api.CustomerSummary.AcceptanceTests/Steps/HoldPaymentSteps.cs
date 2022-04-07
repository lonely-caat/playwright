using System;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Zip.Api.CustomerSummary.Api;
using Zip.Api.CustomerSummary.Application.Payments.Command.HoldPayment;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Steps
{
    [Binding]
    public class HoldPaymentSteps : BaseStep
    {
        public HoldPaymentSteps(TestWebApplicationFactory<TestStartup> factory) : base(factory)
        {
        }

        [Given(@"I have the following AccountId")]
        public void GivenIHaveTheFollowingAccountId()
        {
        }
        
        [Given(@"HoldDate")]
        public void GivenHoldDate()
        {
        }
        
        [When(@"I make a post request to /accounts/id/holdpayment")]
        public async Task WhenIMakeAPostRequestToAccountsIdHoldpayment(Table table)
        {
            foreach (var testItem in table.Rows)
            {
                var accountId = int.Parse(testItem[0]);
                var holdDate = DateTime.Now.Date.AddDays(int.Parse(testItem[1]));
                var expected = Enum.Parse<HttpStatusCode>(testItem[2]);
                var holdPaymentCommand = new HoldPaymentCommand
                {
                    AccountId = accountId,
                    HoldDate = holdDate
                };
                
                response = await _httpClient.PostAsync("/api/accounts/holdpayment", holdPaymentCommand.ToJsonHttpContent());

                Assert.Equal(expected, response.StatusCode);
            }
        }
        
        [Then(@"the response status code should be same as expected")]
        public void ThenTheResponseStatusCodeShouldBeSameAsExpected()
        {
        }
    }
}
