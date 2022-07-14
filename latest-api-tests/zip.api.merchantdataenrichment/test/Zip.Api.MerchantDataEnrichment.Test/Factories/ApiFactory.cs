using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace Zip.Api.MerchantDataEnrichment.Test.Factories
{
    public class ApiFactory<TStartup>
        : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder), "should be specified");
            }

            var configuration = new ConfigurationBuilder()
                               .AddJsonFile("appsettings.json")
                               .Build();

            builder.UseConfiguration(configuration)
                   .UseKestrel()
                   .UseSolutionRelativeContentRoot("")
                   .UseStartup<TestStartup>()
                   .ConfigureTestServices(services =>
                    {
                        // customise DI
                    });
        }
    }
}