using System;
using System.Threading.Tasks;
using Amazon;
using Amazon.Kinesis;
using Amazon.SecurityToken;
using Amazon.SecurityToken.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.KinesisProducer.Interfaces;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.KinesisProducer
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddKinesis(
            this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment env)
        {
            services.AddTransient<IKinesisProducer>(sp =>
            {
                var kinesisSettings = sp.GetRequiredService<IOptions<KinesisSettings>>().Value;

                if (env.IsEnvironment("local") || env.IsEnvironment("dev"))
                {
                    return new KinesisProducer(
                        new AmazonKinesisClient(
                            kinesisSettings.AccessKeyId,
                            kinesisSettings.SecretAccessKey,
                            RegionEndpoint.GetBySystemName(kinesisSettings.Region)));
                }

                var credentials = GetTemporaryCredentialsAsync(sp, kinesisSettings).Result;
                var regionEndpoint = RegionEndpoint.GetBySystemName(kinesisSettings.Region);

                var kinesisClient = new AmazonKinesisClient(
                    credentials.AccessKeyId,
                    credentials.SecretAccessKey,
                    credentials.SessionToken,
                    regionEndpoint);

                return new KinesisProducer(kinesisClient);
            });

            return services;
        }

        private static async Task<Credentials> GetTemporaryCredentialsAsync(IServiceProvider sp, KinesisSettings kinesisSettings)
        {
            var stsClient = sp.GetRequiredService<IAmazonSecurityTokenService>();
            var response = await stsClient.AssumeRoleAsync(
                new AssumeRoleRequest
                {
                    DurationSeconds = kinesisSettings.Duration,
                    RoleArn = kinesisSettings.RoleArn,
                    RoleSessionName = kinesisSettings.RoleSessionName
                });

            var credentials = response.Credentials;
            
            return credentials;
        }
    }
}
