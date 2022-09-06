using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.Http;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Configuration.Handlers
{
    public class ValidateHeadersHandlerTest : ValidateHeadersHandler
    {
        [Fact]
        public async Task Given_NullRequesst_ShouldReturn_HttpResponseMessage()
        {
            var response = await SendAsync(null, CancellationToken.None);
            Assert.IsType<HttpResponseMessage>(response);

            var contentString = await response.Content.ReadAsStringAsync();

            Assert.Equal("Request is null", contentString);
        }

        [Fact]
        public async Task Given_RequestWithHeader_ShouldThrow_InvalidOperationException()
        {
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
           {
               var request = new HttpRequestMessage();
               request.Headers.Add("X-API-KEY", Guid.NewGuid().ToString());
               await SendAsync(request, It.IsAny<CancellationToken>());
           });

            Assert.NotNull(ex);
        }

        [Fact]
        public async Task Given_NoHeaderInRequest_ShouldReturn_HttpResponseMessage()
        {
            var response = await SendAsync(new HttpRequestMessage(), It.IsAny<CancellationToken>());
            Assert.IsType<HttpResponseMessage>(response);

            var contentString = await response.Content.ReadAsStringAsync();

            Assert.Equal("You must supply an API key header called X-API-KEY", contentString);
        }
    }
}
