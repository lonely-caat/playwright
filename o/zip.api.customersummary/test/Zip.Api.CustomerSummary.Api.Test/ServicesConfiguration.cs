using System;
using System.Net.Http;
using System.Net.Http.Headers;
using GraphQL.Client;
using GraphQL.Client.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SendGrid;
using Zip.Api.CustomerSummary.Api.Test.Mocks.Services;
using Zip.Api.CustomerSummary.Infrastructure.Common.ResiliencePolicies;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountSearchService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.ApplicationEventService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CustomerProfileService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.GoogleService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.KinesisProducer.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.KleberService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.PayNowUrlGenerator.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Crm;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Customers;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Payments;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Tango;
using Zip.Api.CustomerSummary.Infrastructure.Services.SmsService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.StatementService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.PaymentWebhookService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.ZipUrlShorteningService.Interfaces;
using Zip.Services.Accounts.ServiceProxy;

namespace Zip.Api.CustomerSummary.Api.Test
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddTestInfrastructureServices(this IServiceCollection services)
        {
            services
                .AddPolly()
                .AddAccountSearchServiceClient()
                .AddRefitClients()
                .AddIGraphQLClient()
                .AddEmailService()
                .AddPayNowUrlGenerator()
                .AddAwsServices()
                .AddEventBus()
                .AddKinesis();

            services
                .AddTransient<IApplicationEventService, MockApplicationEventService>()
                .AddTransient<IAddressValidator, MockAddressValidator>()
                .AddTransient<IAddressSearch, MockAddressSearch>()
                .AddTransient<IZipUrlShorteningService, MockZipUrlShorteningService>()
                .AddTransient<IStatementsService, MockStatementService>()
                .AddTransient<ICustomerProfileService, MockCustomerProfileService>()
                .AddTransient<ISmsService, MockSmsService>()
                .AddTransient<IPaymentWebhookService, MockPaymentWebhookService>()
                .AddTransient<IVcnCardService, MockVcnCardService>()
                .AddTransient<IAccountsService, MockAccountsService>();
            
            
            //        .AddScoped<IIdentityService, IdentityService>()
            //        .AddTransient<ICommunicationsService, CommunicationsService>()
            //        .AddTransient<IVcnTransactionService, VcnTransactionService>();

            return services;
        }

        private static IServiceCollection AddAccountSearchServiceClient(this IServiceCollection services)
        {
            return services.AddScoped<IAccountSearchServiceClient, MockAccountSearchServiceClient>();
        }

        private static IServiceCollection AddRefitClients(this IServiceCollection services)
        {
            return services.AddTransient<IAccountsProxy, MockAccountsProxy>()
                           .AddTransient<ITangoProxy, MockTangoProxy>()
                           .AddTransient<ICommunicationsServiceProxy, MockCommunicationsServiceProxy>()
                           .AddTransient<IPaymentsServiceProxy, MockPaymentsServiceProxy>()
                           .AddTransient<ICustomersServiceProxy, MockCustomersServiceProxy>()
                           .AddTransient<ICrmServiceProxy, MockCrmServiceProxy>();
        }

        private static IServiceCollection AddPayNowUrlGenerator(this IServiceCollection services)
        {
            return services.AddTransient<IPayNowUrlGenerator, MockPayNowUrlGenerator>();
        }

        private static IServiceCollection AddIGraphQLClient(this IServiceCollection services)
        {
            services.AddSingleton<IGraphQLClient>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<CustomerProfileApiOptions>>();
                return new GraphQLHttpClient(
                    new GraphQLHttpClientOptions
                    {
                        EndPoint = new Uri(options.Value.BaseUrl),
                        HttpMessageHandler = new HttpClientHandler
                        {
                            ClientCertificateOptions = ClientCertificateOption.Manual,
                            ServerCertificateCustomValidationCallback = (httpRequestMessage, certificate, certificateChain, policyErrors) => true
                        },
                        MediaType = new MediaTypeHeaderValue("application/json")
                    });
            });
            return services;
        }

        private static IServiceCollection AddEmailService(this IServiceCollection services)
        {
            return services.AddTransient<ISendGridClient, MockSendGridClient>();
        }

        private static IServiceCollection AddAwsServices(this IServiceCollection services)
        {
            return services;
        }

        private static IServiceCollection AddEventBus(this IServiceCollection services)
        {
            return services.AddTransient<IEventBus, MockEventBus>();
        }

        private static IServiceCollection AddKinesis(this IServiceCollection services)
        {
            return services.AddTransient<IKinesisProducer, MockKinesisProducer>();
        }

        private static IServiceCollection AddPolly(this IServiceCollection services)
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