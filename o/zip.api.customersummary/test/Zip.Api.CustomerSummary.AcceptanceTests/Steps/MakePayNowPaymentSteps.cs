using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Zip.Api.CustomerSummary.Api;
using Zip.Api.CustomerSummary.Application.Payments.Command.PayNow;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Steps
{
    [Binding]
    public class MakePayNowPaymentSteps : BaseStep
    {
        public MakePayNowPaymentSteps(TestWebApplicationFactory<TestStartup> factory) : base(factory)
        {
        }

        [When(@"I make a post request to /payments/paynow with following test cases")]
        public async Task WhenIMakeAPostRequestToPaymentsPaynowWithFollowingTestCases(Table table)
        {
            foreach(var testItem in table.Rows)
            {
                var payNowCommand = new PayNowCommand
                {
                    Amount = decimal.Parse(testItem["amount"]),
                    ConsumerId = long.Parse(testItem["consumerId"]),
                    OriginatorEmail = testItem["email"],
                    OriginatorIpAddress = testItem["ip"]
                };
                
                response = await _httpClient.PostAsync("/api/payments/paynow", payNowCommand.ToJsonHttpContent());

                Compare(testItem);
            }
        }
    }
}
