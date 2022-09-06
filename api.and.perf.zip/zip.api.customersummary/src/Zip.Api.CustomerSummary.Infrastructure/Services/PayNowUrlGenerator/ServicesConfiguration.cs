using Microsoft.Extensions.DependencyInjection;
using Zip.Api.CustomerSummary.Infrastructure.Configuration.OutgoingMessages;
using Zip.Api.CustomerSummary.Infrastructure.Services.PayNowUrlGenerator.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Payments;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.PayNow;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.PayNowUrlGenerator
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddPayNowUrlGenerator(this IServiceCollection services)
        {
            return services.AddScoped<IPayNowUrlGenerator>(sp =>
            {
                var notificationConfig = sp.GetRequiredService<IOutgoingMessagesConfig>();

                if (notificationConfig.NewPayNowGenerator)
                {
                    var paynowlinkServiceProxy = sp.GetRequiredService<IPayNowLinkServiceProxy>();

                    return new PayNowUrlNewGenerator(paynowlinkServiceProxy);
                }
                var paymentsServiceProxy = sp.GetRequiredService<IPaymentsServiceProxy>();

                return new Services.PayNowUrlGenerator.PayNowUrlGenerator(paymentsServiceProxy, notificationConfig);
            });
        }
    }
}
