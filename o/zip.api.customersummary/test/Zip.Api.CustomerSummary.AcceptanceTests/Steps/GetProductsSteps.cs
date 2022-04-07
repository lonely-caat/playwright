using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Zip.Api.CustomerSummary.Api;
using Zip.Api.CustomerSummary.Domain.Entities.Products;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Steps
{
    [Binding]
    public class GetProductsSteps : BaseStep
    {
        public GetProductsSteps(TestWebApplicationFactory<TestStartup> factory) : base(factory)
        {

        }

        [When(@"I make a get request to /products")]
        public async Task WhenIMakeAGetRequestToProducts()
        {
            response = await _httpClient.GetAsync("/api/products");
        }

        [Then(@"the response status code should be 200 OK")]
        public void ThenTheResponseStatusCodeShouldBeOK()
        {
            IsResponseOK();
        }

        [Then(@"the response content should be products")]
        public async Task ThenTheResponseContentShouldBeProducts()
        {
            var content = await response.Content.ReadAsStringAsync();
            Assert.NotNull(content);

            var results = JsonConvert.DeserializeObject<IEnumerable<Product>>(content);

            foreach (var r in results)
            {
                Assert.IsType<Product>(r);
            }
        }
    }
}
