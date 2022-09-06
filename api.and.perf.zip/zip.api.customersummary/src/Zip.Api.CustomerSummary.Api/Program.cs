using Zip.Api.CustomerSummary.Api.Extensions;

namespace Zip.Api.CustomerSummary.Api
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Serilog;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    public static class Program
    {
        [SuppressMessage(
            "Microsoft.Design",
            "CA1031:DoNotCatchGeneralExceptionTypes",
            Justification = "Log fatal errors rather than throw")]
        public static int Main(string[] args)
        {
            var loggerConfiguration = LoggingConfiguration.ConfigureLogger();

            Log.Logger = loggerConfiguration.CreateLogger();

            try
            {
                Log.Information("Starting web host");
                CreateWebHostBuilder(args).Build().Run();
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

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    config.AddJsonFile($"appsettings.{env.EnvironmentName.ToLower()}.json", true);
                    config.AddEnvironmentVariables();

                    var vaultConfig = config.Build().GetSection("Vault");

                    if (vaultConfig?["Enabled"] != null &&
                        vaultConfig["Enabled"].ToString().Equals("true", StringComparison.OrdinalIgnoreCase))
                    {
                        Log.Information("Vault is enabled.");
                        var vaultUrI = vaultConfig["Url"];
                        var tokenPath = vaultConfig["TokenPath"];
                        var rdsSecretPath = vaultConfig["SecretPath"];

                        config.AddVaultConfiguration(vaultUrI, tokenPath, rdsSecretPath);
                    }
                })
                .UseStartup<Startup>()
                .UseSerilog();
    }
}