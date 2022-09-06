using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Zip.Api.CustomerSummary.Api.Test.Mocks.Services
{
    public class MockSendGridClient : ISendGridClient
    {
        public MockSendGridClient()
        {
        }

        public string UrlPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Version { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string MediaType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public AuthenticationHeaderValue AddAuthorization(KeyValuePair<string, string> header)
        {
            return new AuthenticationHeaderValue("test");
        }

        public Task<Response> MakeRequest(HttpRequestMessage request, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new Response(System.Net.HttpStatusCode.OK, new StringContent("Hello"), null));
        }

        public Task<Response> RequestAsync(SendGridClient.Method method, string requestBody = null, string queryParams = null, string urlPath = null, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new Response(System.Net.HttpStatusCode.OK, new StringContent("Hello"), null));
        }

        public Task<Response> SendEmailAsync(SendGridMessage msg, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new Response(System.Net.HttpStatusCode.OK, new StringContent("Hello"), null));
        }
    }
}
