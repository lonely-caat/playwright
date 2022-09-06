using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Zip.Api.CustomerSummary.Infrastructure.Common.ResiliencePolicies;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountSearchService.Interfaces;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.AccountSearchService
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddAccountSearchServiceClient(this IServiceCollection services)
        {
            services.AddScoped<IAccountSearchServiceClient>(sp => sp.GetRequiredService<AccountSearchServiceClient>());

            services.AddHttpClient<AccountSearchServiceClient>()
                    .ConfigureHttpClient((sp, client) =>
                     {
                         var options = sp.GetRequiredService<IOptions<AccountSearchSettings>>();
                         var settings = options.Value;
                         client.BaseAddress = settings.BaseUrl;
                     })
                    .ConfigurePrimaryHttpMessageHandler(() =>
                     {
                         var handler = new HttpClientHandler
                         {
                             ClientCertificateOptions = ClientCertificateOption.Manual,
                             ServerCertificateCustomValidationCallback = (m, c, cc, pe) => true
                         };
                         return handler;
                     })
                    .AddPolicyHandler(request => request.Method == HttpMethod.Get ? Policies.ShortTimeout : Policies.LongTimeout)
                    .AddPolicyHandlerFromRegistry("circuitBreaker")
                    .AddPolicyHandlerFromRegistry("bulkhead")
                    .AddPolicyHandlerFromRegistry("RetryPolicy");
            
            return services;
        }
    }
}
