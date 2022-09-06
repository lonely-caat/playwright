using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Zip.Api.CustomerProfile.Helpers;
using Zip.Api.CustomerProfile.Services;
using Zip.Api.CustomerProfile.Vault;
using Zip.Core.Vault;

namespace Zip.Api.CustomerProfile
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            var loggerConfiguration = LoggingConfiguration.ConfigureLogger();

            Log.Logger = loggerConfiguration.CreateLogger();

            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseKestrel(options => { options.AllowSynchronousIO = true; })
                        .UseStartup<Startup>();
                })
                .ConfigureAppConfiguration((context, config) =>
                {
                    var env = context.HostingEnvironment;
                    if (string.IsNullOrEmpty(env.EnvironmentName) || string.Equals(env.EnvironmentName, "development",
                        StringComparison.CurrentCultureIgnoreCase))
                        config
                            .AddJsonFile("appsettings.json", true);
                    else
                        config
                            .AddJsonFile("appsettings.json", false, true)
                            .AddJsonFile($"appsettings.{env.EnvironmentName.ToLower()}.json", true);

                    config
                        .AddEnvironmentVariables()
                        .AddVaultConfiguration(new VaultConfigTransformer());
                })
                .ConfigureServices(services => { services.AddHostedService<HeartbeatPublisher>(); })
                .UseSerilog();
        }
    }
}