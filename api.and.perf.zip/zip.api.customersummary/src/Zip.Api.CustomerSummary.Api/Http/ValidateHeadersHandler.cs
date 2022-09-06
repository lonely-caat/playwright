using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Zip.Api.CustomerSummary.Api.Http
{
    public class ValidateHeadersHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(
                    "Request is null")
                };
            }

            if (request.Headers.Contains("X-API-KEY"))
            {
                return await base.SendAsync(request, cancellationToken);
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(
                    "You must supply an API key header called X-API-KEY")
            };
        }
    }
}
