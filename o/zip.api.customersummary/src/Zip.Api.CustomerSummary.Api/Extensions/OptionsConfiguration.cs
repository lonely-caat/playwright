using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Zip.Api.CustomerSummary.Api.Chaos;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Configuration.EmailSettings;
using Zip.Api.CustomerSummary.Infrastructure.Configuration.OutgoingMessages;

namespace Zip.Api.CustomerSummary.Api.Extensions
{
    public static class OptionsConfiguration
    {
        public static IServiceCollection AddOptionsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration);
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "Please refer to the errors property for additional details.",
                    };

                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json", "application/problem+xml" },
                    };
                };
            });

            services.Configure<OutgoingMessagesConfig>(configuration.GetSection("OutgoingMessages"));
            services.AddSingleton<IOutgoingMessagesConfig>(sp => sp.GetRequiredService<IOptions<OutgoingMessagesConfig>>().Value);
            services.Configure<VaultOptions>(configuration.GetSection("Vault"));
            services.Configure<AppChaosSettings>(configuration.GetSection("ChaosSettings"));
            services.Configure<GoogleSettings>(configuration.GetSection("Google"));
            services.Configure<LmsApiSettings>(configuration.GetSection(nameof(LmsApiSettings)));
            services.Configure<CustomerProfileApiOptions>(configuration.GetSection(nameof(CustomerProfileApiOptions)));
            services.Configure<AccountSearchSettings>(configuration.GetSection(nameof(AccountSearchSettings)));
            services.Configure<CommunicationsServiceProxyOptions>(configuration.GetSection(nameof(CommunicationsServiceProxyOptions)));
            services.Configure<CrmServiceProxyOptions>(configuration.GetSection(nameof(CrmServiceProxyOptions)));
            services.Configure<TwilioSettings>(configuration.GetSection(nameof(TwilioSettings)));
            services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));
            services.Configure<EventBusSettings>(configuration.GetSection(nameof(EventBusSettings)));
            services.Configure<KinesisSettings>(configuration.GetSection(nameof(KinesisSettings)));
            services.Configure<AddressVerificationProxyOptions>(configuration.GetSection(nameof(AddressVerificationProxyOptions)));
            services.Configure<StatementsApiProxyOptions>(configuration.GetSection(nameof(StatementsApiProxyOptions)));
            services.Configure<BeamApiProxyOptions>(configuration.GetSection(nameof(BeamApiProxyOptions)));
            services.Configure<MfaApiProxyOptions>(configuration.GetSection(nameof(MfaApiProxyOptions)));
            services.Configure<MerchantDashBoardApiOptions>(configuration.GetSection(nameof(MerchantDashBoardApiOptions)));
            services.Configure<CoreApiProxyOptions>(configuration.GetSection(nameof(CoreApiProxyOptions)));
            services.Configure<CoreGraphProxyOptions>(configuration.GetSection(nameof(CoreGraphProxyOptions)));
            services.Configure<CustomerCoreApiProxyOptions>(configuration.GetSection(nameof(CustomerCoreApiProxyOptions)));
            
            return services;
        }
    }
}
