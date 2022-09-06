using System;
using System.Threading.Tasks;
using Amazon;
using Amazon.SecurityToken;
using Amazon.SecurityToken.Model;
using Amazon.SimpleNotificationService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.ApplicationEventService.Interfaces;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.ApplicationEventService
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddEventBus(
            this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment env)
        {
            services.AddTransient<INamingStrategy, NamingStrategy>()
                    .AddTransient<IEventBus>(sp =>
                    {
                        var settings = sp.GetRequiredService<IOptions<EventBusSettings>>()
                                         .Value;
                        var namingStrategy = sp.GetRequiredService<INamingStrategy>();

                        if (env.IsEnvironment("local") || env.IsEnvironment("dev"))
                        {
                            var snsClient = new AmazonSimpleNotificationServiceClient(
                                configuration["KinesisSettings:AccessKeyId"],
                                configuration["KinesisSettings:SecretAccessKey"],
                                RegionEndpoint.APSoutheast2);

                            return new EventBusAmazonSns(snsClient, namingStrategy, settings);
                        }

                        var credentials = GetTemporaryCredentialsAsync(sp, settings).Result;

                        var regionEndpoint = RegionEndpoint.GetBySystemName(settings.Region);

                        var sns = new AmazonSimpleNotificationServiceClient(credentials.AccessKeyId,
                                                                            credentials.SecretAccessKey,
                                                                            credentials.SessionToken,
                                                                            regionEndpoint);

                        return new EventBusAmazonSns(sns, namingStrategy, settings);
                    });

            return services;
        }

        private static async Task<Credentials> GetTemporaryCredentialsAsync(IServiceProvider sp, EventBusSettings settings)
        {
            var stsClient = sp.GetRequiredService<IAmazonSecurityTokenService>();
            var response =
                          await stsClient.AssumeRoleAsync(new AssumeRoleRequest
                          {
                              DurationSeconds = settings.Duration,
                              RoleArn = settings.RoleArn,
                              RoleSessionName = settings.RoleSessionName
                          });

            var credentials = response.Credentials;

            return credentials;
        }
    }
}
