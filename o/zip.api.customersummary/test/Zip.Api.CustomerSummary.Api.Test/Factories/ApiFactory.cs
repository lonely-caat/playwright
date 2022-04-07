using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Zip.Api.CustomerSummary.Api.Test.Factories
{
    public class ApiFactory: WebApplicationFactory<ApiTestStartup>
    {
        protected Action<IServiceCollection> ConfigureTestServices;
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
                .UseStartup<ApiTestStartup>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException($"{nameof(builder)} should be specified");
            }
            
            builder
                .UseKestrel()
                .UseSolutionRelativeContentRoot("")
                .UseEnvironment("test")
                .UseStartup<ApiTestStartup>()
                .ConfigureServices(services =>
                {
                    ConfigureTestServices?.Invoke(services);
                });
        }

        internal HttpClient CreateClient(Action<IServiceCollection> configureServices)
        {
            ConfigureTestServices = configureServices;
            return CreateClient();
        }
    }
}