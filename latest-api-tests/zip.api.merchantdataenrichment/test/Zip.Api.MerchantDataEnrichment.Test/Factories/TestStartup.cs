using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zip.Api.MerchantDataEnrichment.Extensions;
using Zip.Api.MerchantDataEnrichment.Test.Factories.MockServices;
using Zip.MerchantDataEnrichment.Application;
using Zip.MerchantDataEnrichment.Infrastructure;
using Zip.MerchantDataEnrichment.Infrastructure.Services.MerchantLookupService.Interfaces;
using Zip.MerchantDataEnrichment.Persistence.DbContexts;
using Zip.Partners.Core.Kafka.MessageBus;
using Zip.Partners.Core.Kafka.MessageBus.Extensions;
using Zip.Partners.Core.Kafka.MessageBus.TestInfrastructure;
using ApplicationServicesConfiguration = Zip.MerchantDataEnrichment.Application.ServicesConfiguration;

namespace Zip.Api.MerchantDataEnrichment.Test.Factories
{
    public class TestStartup : Startup
    {
        // database root is required for in-memory database data persistence
        private readonly InMemoryDatabaseRoot _databaseRoot = new InMemoryDatabaseRoot();

        public TestStartup(IConfiguration configuration, IWebHostEnvironment environment) : base(configuration, environment)
        {
        }

        protected override IServiceCollection ConfigureAllServices(IServiceCollection services)
        {
            services.AddApplicationServices(Configuration)
                    .AddInfrastructureServices(Configuration)
                    .AddEntityFrameworkInMemoryDatabase()
                    .AddDbContext<MerchantDataEnrichmentDbContext>((sp, options) =>
                     {
                         options.UseInMemoryDatabase("InMemory_DB", _databaseRoot)
                                .UseInternalServiceProvider(sp);
                     })
                    .AddScoped<IMerchantDataEnrichmentDbContext, MerchantDataEnrichmentDbContext>()
                    .AddDefaultMessageBus(Configuration, typeof(ApplicationServicesConfiguration).Assembly);

            services.Replace<IMerchantLookupService>(sp => new MockMerchantLookupService(), ServiceLifetime.Scoped);
            services.Replace<IBus>(sp => new NoOpBus(), ServiceLifetime.Singleton);

            return services;
        }

        protected override void EnsureDatabaseCreated(MerchantDataEnrichmentDbContext context)
        {
            context.Database.EnsureCreated();
            context.PopulateTestData();
        }
    }
}