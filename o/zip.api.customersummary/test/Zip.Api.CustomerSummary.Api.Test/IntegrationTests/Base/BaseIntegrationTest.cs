using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Factories;

namespace Zip.Api.CustomerSummary.Api.Test.IntegrationTests.Base
{
    public abstract class BaseIntegrationTest : IClassFixture<ApiFactory> 
    {
        protected ApiFactory Factory { get; }
        protected HttpClient Client { get; set; }

        protected BaseIntegrationTest(
            ApiFactory factory)
        {
            Factory = factory;
            Client = factory.CreateClient(ConfigureTestServices);
        }

        protected virtual void ConfigureTestServices(IServiceCollection services)
        {
        }

        protected StringContent Content(object value, string mediaType = "application/json")
        {
            var json = JsonConvert.SerializeObject(value ?? "");
            return new StringContent(json, Encoding.UTF8, mediaType);
        }

        [DebuggerStepThrough]
        internal Task<HttpResponseMessage> SendRequest(string url, string method = "GET", object content = null)
        {
            switch (method)
            {
                case "DELETE":
                    return Client.DeleteAsync(new Uri(url, UriKind.Relative));
                case "POST":
                    return PostAsync(url, content);
                case "PUT":
                    return PutAsync(url, content);
                default:
                    return Client.GetAsync(new Uri(url, UriKind.Relative));
            }
        }

        protected Task<HttpResponseMessage> PutAsync(string url, object content)
        {
            var stringContent = Content(content);
            return Client.PutAsync(new Uri(url, UriKind.Relative), stringContent);
        }

        protected Task<HttpResponseMessage> PostAsync(string url, object content)
        {
            var stringContent = Content(content);
            return Client.PostAsync(new Uri(url, UriKind.Relative), stringContent);
        }

        protected void TestDependency<T>()
        {
            var service = Factory.Services.GetService<T>();
            service.Should().NotBeNull();
        }

        protected void TestDependencies(params Type[] types)
        {
            foreach (var type in types)
            {
                var service = Factory.Services.GetService(type);
                service.Should().NotBeNull($"Unable to resolve service of type '{type.Name}'");
            }
        }
    }
}
