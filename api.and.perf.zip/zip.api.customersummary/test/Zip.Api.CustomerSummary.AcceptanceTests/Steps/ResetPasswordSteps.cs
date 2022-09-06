using System;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Zip.Api.CustomerSummary.Api;
using Zip.Api.CustomerSummary.Application.Communications.Command.SendResetPasswordEmailNew;
using Zip.Api.CustomerSummary.Domain.Entities.Products;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Steps
{
    [Binding]
    public class ResetPasswordSteps : BaseStep
    {
        public ResetPasswordSteps(TestWebApplicationFactory<TestStartup> factory) : base(factory)
        {
        }

        [When(@"I make a post request to /contacts/password/reset with following test cases")]
        public async Task WhenIMakeAPostRequestToContactsPasswordResetWithFollowingTestCases(Table table)
        {
            foreach (var testItem in table.Rows)
            {
                var sendResetPasswordEmailNewCommand = new SendResetPasswordEmailNewCommand
                {
                    Email = testItem["email"],
                    Classification = Enum.TryParse<ProductClassification>(testItem["classification"], out var r) ? r : null as ProductClassification?,
                    FirstName = testItem["firstName"]
                };
                
                response = await _httpClient.PostAsync("/api/contacts/password/reset", sendResetPasswordEmailNewCommand.ToJsonHttpContent());


                Assert.Equal(Enum.Parse<HttpStatusCode>(testItem["expected"]), response.StatusCode);
            }
        }
    }
}
