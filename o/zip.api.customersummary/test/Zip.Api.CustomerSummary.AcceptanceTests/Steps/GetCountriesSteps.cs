using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Zip.Api.CustomerSummary.Api;
using Zip.Api.CustomerSummary.Domain.Entities.Countries;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Steps
{
    [Binding]
    public class GetCountriesSteps : BaseStep
    {
        public GetCountriesSteps(TestWebApplicationFactory<TestStartup> factory) : base(factory)
        {

        }

        [When(@"I make a get request to /countries")]
        public async Task WhenIMakeAGetRequestToCountries()
        {
            response = await _httpClient.GetAsync("/api/countries");
        }

        [Then(@"the response status code is 200 OK")]
        public void ThenTheResponseStatusCodeIsOK()
        {
            IsResponseOK();
        }

        [Then(@"the response content are countries")]
        public async Task ThenTheResponseContentAreCountries()
        {
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<IEnumerable<Country>>(content);

            foreach (var r in results)
            {
                Assert.IsType<Country>(r);
            }
        }
    }
}
