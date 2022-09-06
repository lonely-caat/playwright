using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Zip.Api.CustomerSummary.Api.Test.Common
{
    public class MockHttpMessageHandler : DelegatingHandler
    {
        private readonly HttpResponseMessage _mockResponse;

        public MockHttpMessageHandler(HttpResponseMessage responseMessage)
        {
            _mockResponse = responseMessage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_mockResponse);
        }
    }
}
