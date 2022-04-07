using System;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Zip.Api.CustomerSummary.Api;
using Zip.Api.CustomerSummary.Application.Communications.Command.SendPayNowLink;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Steps
{
    [Binding]
    public class SendPayNowLinkSteps : BaseStep
    {
        public SendPayNowLinkSteps(TestWebApplicationFactory<TestStartup> factory) : base(factory)
        {
        }

        [When(@"I make a post request to /outgoingmessages/paynowlink/sms with following test cases")]
        public async Task WhenIMakeAPostRequestToOutgoingMessagesPaynowlinkSmsWithFollowingTestCases(Table table)
        {
            foreach (var testItem in table.Rows)
            {
                var sendPayNowLinkCommand = new SendPayNowLinkCommand
                {
                    Amount = decimal.Parse(testItem["amount"]),
                    ConsumerId = long.Parse(testItem["consumerId"])
                };
                
                response = await _httpClient.PostAsync("/api/outgoingmessages/paynowlink/sms", sendPayNowLinkCommand.ToJsonHttpContent());

                Assert.Equal(Enum.Parse<HttpStatusCode>(testItem["expected"]), response.StatusCode);
            }
        }
    }
}
