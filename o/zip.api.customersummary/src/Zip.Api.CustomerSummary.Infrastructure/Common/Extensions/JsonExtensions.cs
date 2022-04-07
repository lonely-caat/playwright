using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJsonString<T>(this T data)
        {
            if (data != null)
            {
                return JsonConvert.SerializeObject(data);
            }
                
            throw new ArgumentNullException(nameof(data));
        }

        public static HttpContent ToJsonHttpContent<T>(this T data)
        {
            if (data != null)
            {
                return new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            }

            throw new ArgumentNullException(nameof(data));
        }
    }
}
