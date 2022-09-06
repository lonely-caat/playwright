using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using TechTalk.SpecFlow;
using Xunit;
using Zip.Api.CustomerSummary.Api;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Steps
{
    [Binding]
    public class BaseStep : IClassFixture<TestWebApplicationFactory<TestStartup>>
    {
        private readonly TestWebApplicationFactory<TestStartup> _factory;
        protected readonly HttpClient _httpClient;
        protected HttpResponseMessage response;

        public BaseStep(TestWebApplicationFactory<TestStartup> factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:3920");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "TOKEN");
        }

        protected void IsResponseOK()
        {
            VerifyResponseStatusCode(HttpStatusCode.OK);
        }

        protected void VerifyResponseStatusCode(HttpStatusCode httpStatusCode)
        {
            Assert.Equal(httpStatusCode, response?.StatusCode);
        }

        protected void Compare(TableRow row)=> Assert.Equal(Enum.Parse<HttpStatusCode>(row["expected"]), response.StatusCode);
    }
}
