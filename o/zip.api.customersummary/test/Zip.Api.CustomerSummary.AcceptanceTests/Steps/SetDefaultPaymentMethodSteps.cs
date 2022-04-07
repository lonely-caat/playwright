using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Zip.Api.CustomerSummary.Api;
using Zip.Api.CustomerSummary.Application.Payments.Command.SetDefaultPaymentMethod;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Steps
{
    [Binding]
    public class SetDefaultPaymentMethodSteps : BaseStep
    {
        public SetDefaultPaymentMethodSteps(TestWebApplicationFactory<TestStartup> factory) : base(factory)
        {
        }

        [When(@"I make a post request to /paymentmethods/id/default with following test cases")]
        public async Task WhenIMakeAPostRequestToPaymentmethodsIdDefaultWithFollowingTestCases(Table table)
        {
            foreach (var testItem in table.Rows)
            {
                var paymentMethodId = Guid.TryParse(testItem["paymentMethodId"], out var r) ? r : Guid.Empty;
                var consumerId = long.Parse(testItem["consumerId"]);
                var setDefaultPaymentMethodCommand = new SetDefaultPaymentMethodCommand
                {
                    ConsumerId = consumerId,
                    PaymentMethodId = paymentMethodId
                };

                response = await _httpClient.PostAsync($"/api/paymentmethods/{paymentMethodId}/default",
                                                       setDefaultPaymentMethodCommand.ToJsonHttpContent());

                Compare(testItem);
            }
        }
    }
}
