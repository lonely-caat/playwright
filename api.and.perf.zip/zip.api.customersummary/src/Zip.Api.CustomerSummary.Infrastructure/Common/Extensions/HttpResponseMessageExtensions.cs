using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static void EnsureSuccess(this HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return;
            }

            var content = httpResponseMessage.ReadContent("No Content");
            var uri = httpResponseMessage.RequestMessage.RequestUri;
            throw new HttpRequestException($"Request to {uri} {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase} [{content}]");
        }

        public static string ReadContent(this HttpResponseMessage httpResponseMessage, string @default = null)
        {
            return httpResponseMessage.Content != null
                ? httpResponseMessage.Content.ReadAsStringAsync().Result
                : @default;
        }

        public static async Task<T> DeserializeAsync<T>(this HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.Content == null)
            {
                return default(T);
            }

            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }

        public static bool EnsureSuccessStatusCode(this HttpResponseMessage httpResponseMessage, out string content)
        {
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                content = httpResponseMessage.Content.ReadAsStringAsync().Result;

                return true;
            }

            content = httpResponseMessage.Content != null
                          ? httpResponseMessage.Content.ReadAsStringAsync().Result
                          : "No Content Found";

            return false;
        }

        public static async Task<T> ExtractContent<T>(this HttpResponseMessage httpResponseMessage,
            bool allowNotFound = true)
        {
            var content = string.Empty;
            if (httpResponseMessage.Content != null)
            {
                content = await httpResponseMessage.Content.ReadAsStringAsync();
            }

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(content);
            }

            if (allowNotFound && httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                return default;
            }

            throw new HttpRequestException($"{httpResponseMessage.ReasonPhrase} {content}");
        }
    }
}
