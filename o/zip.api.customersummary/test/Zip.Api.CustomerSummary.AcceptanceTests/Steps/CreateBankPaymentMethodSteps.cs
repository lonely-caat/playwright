using System;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Zip.Api.CustomerSummary.Api;
using Zip.Api.CustomerSummary.Application.Payments.Command.CreateBankPaymentMethod;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Steps
{
    [Binding]
    public class CreateBankPaymentMethodSteps : BaseStep
    {
        public CreateBankPaymentMethodSteps(TestWebApplicationFactory<TestStartup> factory) : base(factory)
        {
        }

        [When(@"I make a post request to /paymentmethods/bank with following test cases")]
        public async Task WhenIMakeAPostRequestToPaymentmethodsBankWithFollowingTestCases(Table table)
        {
            foreach (var testItem in table.Rows)
            {
                var consumerId = long.Parse(testItem["consumerId"]);
                var createBankPaymentMethodCommand = new CreateBankPaymentMethodCommand
                {
                    AccountName = testItem["accountName"],
                    AccountNumber = testItem["accountNumber"],
                    BSB = testItem["bsb"],
                    ConsumerId = consumerId,
                    OriginatorEmail = testItem["originatorEmail"]
                };
                
                response = await _httpClient.PostAsync("/api/paymentmethods/bank", createBankPaymentMethodCommand.ToJsonHttpContent());

                Assert.Equal(Enum.Parse<HttpStatusCode>(testItem["expected"]), response.StatusCode);
            }
        }
    }
}
