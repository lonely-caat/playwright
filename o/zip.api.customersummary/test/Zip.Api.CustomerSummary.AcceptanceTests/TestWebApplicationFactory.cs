using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using GraphQL.Client;
using GraphQL.Client.Http;
using Zip.Api.CustomerSummary.AcceptanceTests.Mocks.DbContexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Zip.Api.CustomerSummary.AcceptanceTests.Mocks.Services;
using Zip.Services.Accounts.ServiceProxy;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Customers;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Payments;
using SendGrid;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountSearchService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.ApplicationEventService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CustomerProfileService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.KinesisProducer.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.KleberService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.PayNowUrlGenerator.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Crm;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Tango;
using Zip.Api.CustomerSummary.Infrastructure.Services.SmsService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.StatementService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.ZipUrlShorteningService.Interfaces;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.AcceptanceTests
{
    public class TestWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder(null)
                          .ConfigureAppConfiguration((hostingContext, config) =>
                           {
                               var projectDirectory = Directory.GetCurrentDirectory();
                               var configPath = Path.Combine(projectDirectory, "appsettings.test.json");

                               config.SetBasePath(projectDirectory);
                               config.AddJsonFile(configPath);
                               config.AddEnvironmentVariables();
                           })
                          .UseStartup<TStartup>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("test")
                   .ConfigureServices(services =>
                    {
                        services.AddTransient<ICountryContext, MockCountryContext>();
                        services.AddTransient<IProductContext, MockProductContext>();
                        services.AddTransient<IAccountContext, MockAccountContext>();
                        services.AddTransient<IAccountTypeContext, MockAccountTypeContext>();
                        services.AddTransient<IConsumerContext, MockConsumerContext>();
                        services.AddTransient<ICustomerAttributeContext, MockCustomerAttributeContext>();
                        services.AddTransient<IAddressContext, MockAddressContext>();
                        services.AddTransient<IMerchantContext, MockMerchantContext>();
                        services.AddTransient<ICreditProfileContext, MockCreditProfileContext>();
                        services.AddTransient<IContactContext, MockContactContext>();
                        services.AddTransient<ICrmCommentContext, MockCrmCommentContext>();
                        services.AddTransient<IMessageLogContext, MockMessageLogContext>();
                        services.AddTransient<ISmsContentContext, MockSmsContentContext>();
                        services.AddTransient<ITransactionHistoryContext, MockTransactionHistoryContext>();
                        services.AddTransient<IStatementContext, MockStatementContext>();
                        services.AddTransient<IPayNowAccountContext, MockPayNowAccountContext>();

                        services.AddTransient<IEventBus, MockEventBus>();
                        services.AddTransient<ISendGridClient, MockSendGridClient>();
                        services.AddTransient<IAccountSearchServiceClient, MockAccountSearchServiceClient>();
                        services.AddTransient<IApplicationEventService, MockApplicationEventService>();
                        services.AddTransient<IAddressValidator, MockAddressValidator>();
                        services.AddTransient<IKinesisProducer, MockKinesisProducer>();
                        services.AddTransient<ISmsService, MockSmsService>();
                        services.AddTransient<IPayNowUrlGenerator, MockPayNowUrlGenerator>();
                        services.AddTransient<IZipUrlShorteningService, MockZipUrlShorteningService>();
                        services.AddTransient<IStatementsService, MockStatementService>();
                        services.AddTransient<ICustomerProfileService, MockCustomerProfileService>();
                        services.AddTransient<ICommunicationsService, MockCommunicationsService>();
                        services.AddTransient<ICoreService, MockCoreService>();

                        services.AddTransient<IAccountsProxy, MockAccountsProxy>();
                        services.AddTransient<ITangoProxy, MockTangoProxy>();
                        services.AddTransient<ICommunicationsServiceProxy, MockCommunicationsServiceProxy>();
                        services.AddTransient<IPaymentsServiceProxy, MockPaymentsServiceProxy>();
                        services.AddTransient<ICustomersServiceProxy, MockCustomersServiceProxy>();
                        services.AddTransient<ICrmServiceProxy, MockCrmServiceProxy>();
                        services.AddTransient<IAccountsService, MockAccountsService>();
                        services.AddTransient<ICoreServiceProxy, MockCoreServiceProxy>();

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
                    });
        }
    }
}
