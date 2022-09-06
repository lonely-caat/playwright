using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Zip.Api.CustomerSummary.Api;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Steps
{
    [Binding]
    public class SearchAccountsSteps : BaseStep
    {
        public SearchAccountsSteps(TestWebApplicationFactory<TestStartup> factory) : base(factory)
        {
        }

        [When(@"I make a get request to /accounts")]
        public async Task WhenIMakeAGetRequestToAccounts()
        {
            response = await _httpClient.GetAsync("/api/accounts");
        }
        
        [Then(@"the response should be 200 OK")]
        public void ThenTheResponseShouldBeOK()
        {
            IsResponseOK();
        }
        
        [Then(@"the response content should be AccountListItems")]
        public async Task ThenTheResponseContentShouldBeAccountListItems()
        {
            var content = await response.Content.ReadAsStringAsync();
            Assert.NotNull(content);

            var results = JsonConvert.DeserializeObject<IEnumerable<AccountListItem>>(content);
            foreach(var r in results)
            {
                Assert.IsType<AccountListItem>(r);
            }
        }
    }
}
