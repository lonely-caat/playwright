using System;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Zip.Api.CustomerSummary.Api;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateContact;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Steps
{
    [Binding]
    public class UpdateContactSteps : BaseStep
    {
        public UpdateContactSteps(TestWebApplicationFactory<TestStartup> factory) : base(factory)
        {
        }

        [When(@"I make a put request to /contacts with following data")]
        public async Task WhenIMakeAPutRequestToContactsWithFollowingData(Table table)
        {
            foreach (var testItem in table.Rows)
            {
                var email = testItem["email"]; 
                var mobile = testItem["mobile"];
                var updateContactCommand = new UpdateContactCommand
                {
                    ConsumerId = long.Parse(testItem["consumerId"]),
                    Email = email,
                    Mobile = mobile
                };
                
                response = await _httpClient.PutAsync("/api/contacts", updateContactCommand.ToJsonHttpContent());

                Assert.Equal(Enum.Parse<HttpStatusCode>(testItem["expected"]), response.StatusCode);
            }
        }
    }
}
