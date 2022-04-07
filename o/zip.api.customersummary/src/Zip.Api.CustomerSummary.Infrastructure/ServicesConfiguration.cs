using Amazon.SecurityToken;
using Amazon.SimpleNotificationService;
using GraphQL.Client;
using GraphQL.Client.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Refit;
using SendGrid;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Zip.Api.CustomerSummary.Infrastructure.Common.ResiliencePolicies;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Configuration.EmailSettings;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountSearchService;
using Zip.Api.CustomerSummary.Infrastructure.Services.ApplicationEventService;
using Zip.Api.CustomerSummary.Infrastructure.Services.ApplicationEventService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.BeamService;
using Zip.Api.CustomerSummary.Infrastructure.Services.BeamService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService;
using Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreService;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CustomerCoreService;
using Zip.Api.CustomerSummary.Infrastructure.Services.CustomerCoreService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CustomerProfileService;
using Zip.Api.CustomerSummary.Infrastructure.Services.CustomerProfileService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.GoogleService;
using Zip.Api.CustomerSummary.Infrastructure.Services.GoogleService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.IdentityService;
using Zip.Api.CustomerSummary.Infrastructure.Services.IdentityService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.KinesisProducer;
using Zip.Api.CustomerSummary.Infrastructure.Services.KleberService;
using Zip.Api.CustomerSummary.Infrastructure.Services.KleberService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.MerchantDashboardService;
using Zip.Api.CustomerSummary.Infrastructure.Services.MerchantDashboardService.Interface;
using Zip.Api.CustomerSummary.Infrastructure.Services.MfaService;
using Zip.Api.CustomerSummary.Infrastructure.Services.MfaService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.PayNowUrlGenerator;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Crm;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Customers;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Payments;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.PayNow;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Tango;
using Zip.Api.CustomerSummary.Infrastructure.Services.SmsService;
using Zip.Api.CustomerSummary.Infrastructure.Services.SmsService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.StatementService;
using Zip.Api.CustomerSummary.Infrastructure.Services.StatementService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.PaymentWebhookService;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.PaymentWebhookService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.ZipUrlShorteningService;
using Zip.Api.CustomerSummary.Infrastructure.Services.ZipUrlShorteningService.Interfaces;
using Zip.Services.Accounts.ServiceProxy;

namespace Zip.Api.CustomerSummary.Infrastructure
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            services.AddPolly()
                    .AddRefitClients(configuration, environment)
                    .AddIGraphQLClient()
                    .AddEmailService()
                    .AddPayNowUrlGenerator()
                    .AddAccountSearchServiceClient()
                    .AddAwsServices(configuration)
                    .AddEventBus(configuration, environment)
                    .AddKinesis(configuration, environment);

            services.AddScoped<IApplicationEventService, ApplicationEventService>()
                    .AddScoped<IIdentityService, IdentityService>()
                    .AddScoped<IAddressValidator, AddressValidator>()
                    .AddScoped<IBeamService, BeamService>()
                    .AddScoped<IMfaService, MfaService>()
                    .AddScoped<IMerchantDashboardService, MerchantDashboardService>()
                    .AddScoped<ICoreService, CoreService>()
                    .AddScoped<IAccountsService, AccountsService>()
                    .AddScoped<ICoreGraphService, CoreGraphService>()
                    .AddScoped<ICustomerCoreService, CustomerCoreService>();

            services.AddTransient<ISmsClient, TwilioSmsClient>()
                    .AddTransient<ISmsClientContextService, SmsClientContextService>()
                    .AddTransient<ISmsService, SmsService>()
                    .AddTransient<IVcnCardService, VcnCardService>()
                    .AddTransient<IPaymentWebhookService, PaymentWebhookService>()
                    .AddTransient<IAddressSearch, AddressSearchService>()
                    .AddTransient<ICommunicationsService, CommunicationsService>()
                    .AddTransient<IZipUrlShorteningService, ZipUrlShorteningService>()
                    .AddTransient<IStatementsService, StatementsService>()
                    .AddTransient<ICustomerProfileService, CustomerProfileService>();

            return services;
        }

        private static IServiceCollection AddRefitClients(
            this IServiceCollection services,
            IConfiguration configuration,
            IHostEnvironment environment)
        {
            services.AddRefitClient<IUserManagementProxy>()
                    .ConfigureHttpClient(client =>
                     {
                         if (environment.IsEnvironment("local"))
                         {
                             client.BaseAddress = new Uri(configuration["IdentityServiceProxy:LocalBaseUrl"]);
                         }
                         else
                         {
                             var clusterInternalBaseUrl =
                                 Environment.ExpandEnvironmentVariables(
                                     $"%{configuration["IdentityServiceProxy:ClusterInternalUrlVar"]}%");
                             var port = configuration["IdentityServiceProxy:ClusterInternalUrlPort"];

                             client.BaseAddress = new Uri($"http://{clusterInternalBaseUrl}:{port}");
                         }
                     });

            services.AddRefitClient<IVcnCardsApiProxy>()
                    .ConfigureHttpClient(client =>
                     {
                         client.BaseAddress = new Uri(configuration["CardsApiProxy:BaseUrl"]);
                     });


            services.AddRefitClient<ITangoProxy>()
                    .ConfigureHttpClient(client =>
                     {
                         var baseUrl = new Uri(configuration["TangoSettings:Endpoint"]);
                         client.BaseAddress = baseUrl;
                     });

            services.AddRefitClient<IAccountsProxy>()
                    .ConfigureHttpClient(client =>
                     {
                         var baseUrl = new Uri(configuration["AccountProxySettings:BaseUrl"]);
                         client.BaseAddress = baseUrl;
                     });

            services.AddRefitClient<IPaymentsServiceProxy>()
                    .ConfigureHttpClient(client =>
                     {
                         var baseUrl = new Uri(configuration["PaymentsServiceProxy:BaseUrl"]);
                         client.BaseAddress = baseUrl;
                     });

            services.AddRefitClient<ICustomersServiceProxy>()
                    .ConfigureHttpClient(client =>
                     {
                         var baseUrl = new Uri(configuration["CustomersServiceProxy:BaseUrl"]);
                         client.BaseAddress = baseUrl;

                         var token = configuration["CustomersServiceProxy:Authorization"];
                         client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);
                     });

            services.AddRefitClient<ICommunicationsServiceProxy>()
                    .ConfigureHttpClient(client =>
                     {
                         client.BaseAddress =
                             new Uri(configuration["CommunicationsServiceProxyOptions:BaseUrl"]);
                     });

            services.AddRefitClient<ICrmServiceProxy>()
                    .ConfigureHttpClient(client =>
                     {
                         client.BaseAddress =
                             new Uri(configuration["CrmServiceProxyOptions:BaseUrl"]);
                     });

            services.AddRefitClient<IPayNowLinkServiceProxy>()
                    .ConfigureHttpClient(client =>
                     {
                         client.BaseAddress =
                             new Uri(configuration["PayNowLinkServiceProxySettings:BaseUrl"]);
                     });

            services.AddRefitClient<IAddressVerificationProxy>()
                    .ConfigureHttpClient((sp, httpClient) =>
                     {
                         var options = sp.GetRequiredService<IOptions<AddressVerificationProxyOptions>>().Value;
                         httpClient.BaseAddress = options.ServiceUrl;
                     });

            services.AddRefitClient<IStatementsApiProxy>()
                    .ConfigureHttpClient((sp, httpClient) =>
                     {
                         var options = sp.GetRequiredService<IOptions<StatementsApiProxyOptions>>().Value;
                         httpClient.BaseAddress = new Uri(options.BaseUrl);
                     });

            services.AddRefitClient<IPaymentWebhookApiProxy>()
                    .ConfigureHttpClient(client =>
                    {
                        client.BaseAddress = new Uri(configuration["PaymentWebhookApiProxy:BaseUrl"]);
                    });

            services.AddRefitClient<IBeamProxy>()
                    .ConfigureHttpClient((provider, client) =>
                    {
                        var options = provider.GetService<IOptions<BeamApiProxyOptions>>().Value;

                        client.BaseAddress = new Uri(options.BaseUrl);
                        client.DefaultRequestHeaders.From = options.DefaultUser;

                        var token = options.Authorization;
                        var tokenAsBytes = System.Text.Encoding.UTF8.GetBytes(token);
                        var encodedToken = Convert.ToBase64String(tokenAsBytes);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedToken);
                    });

            services.AddRefitClient<IMfaProxy>()
                    .ConfigureHttpClient((sp, httpClient) =>
                    {
                        var options = sp.GetRequiredService<IOptions<MfaApiProxyOptions>>().Value;
                        httpClient.BaseAddress = new Uri(options.BaseUrl);
                    });

            services.AddRefitClient<IMerchantDashboardApiProxy>()
                    .ConfigureHttpClient((sp, httpClient) =>
                     {
                         var options = sp.GetRequiredService<IOptions<MerchantDashBoardApiOptions>>().Value;
                         httpClient.BaseAddress = new Uri(options.BaseUrl);
                         httpClient.DefaultRequestHeaders.Add("ZIP-API-KEY", options.ApiKey);
                     });

            services.AddRefitClient<ICoreServiceProxy>()
                    .ConfigureHttpClient(client =>
                     {
                         client.BaseAddress =
                             new Uri(configuration["CoreApiProxyOptions:BaseUrl"]);
                     });

            services.AddRefitClient<ICoreGraphServiceProxy>()
                    .ConfigureHttpClient((provider, client) =>
                     {
                         var options = provider.GetRequiredService<IOptions<CoreGraphProxyOptions>>().Value;

                         client.BaseAddress = new Uri(options.BaseUrl);
                     });
            
            services.AddRefitClient<ICustomerCoreServiceProxy>()
                    .ConfigureHttpClient((provider, client) =>
                     {
                         var options = provider.GetRequiredService<IOptions<CustomerCoreApiProxyOptions>>()
                                               .Value;
                         client.BaseAddress =
                             new Uri(options.BaseUrl);
                     });
            
            return services;
        }

        private static IServiceCollection AddIGraphQLClient(this IServiceCollection services)
        {
            var customerHandler = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, certificate, certificateChain, policyErrors) => true
            };

            services.AddSingleton<IGraphQLClient>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<CustomerProfileApiOptions>>();

                return new GraphQLHttpClient(
                    new GraphQLHttpClientOptions
                    {
                        EndPoint = new Uri(options.Value.BaseUrl),
                        HttpMessageHandler = customerHandler,
                        MediaType = new MediaTypeHeaderValue("application/json")
                    });
            });

            return services;
        }

        private static IServiceCollection AddEmailService(this IServiceCollection services)
        {
            return services.AddTransient<ISendGridClient>(
                sp =>
                {
                    var options = sp.GetRequiredService<IOptions<EmailSettings>>();
                    var emailSettings = options?.Value;

                    return new SendGridClient(emailSettings?.ApiKey);
                });
        }

        private static IServiceCollection AddAwsServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDefaultAWSOptions(configuration.GetAWSOptions())
                           .AddAWSService<IAmazonSimpleNotificationService>()
                           .AddAWSService<IAmazonSecurityTokenService>();
        }

        public static IServiceCollection AddPolly(this IServiceCollection services)
        {
            var policyRegistry = services.AddPolicyRegistry();

            policyRegistry["RetryPolicy"] = Policies.GetRetryPolicy();
            policyRegistry["circuitBreaker"] = Policies.GetCircuitBreakerPolicy();
            policyRegistry.Add("short", Policies.ShortTimeout);
            policyRegistry.Add("long", Policies.LongTimeout);
            policyRegistry.Add("bulkhead", Policies.Bulkhead);

            return services;
        }
    }
}
