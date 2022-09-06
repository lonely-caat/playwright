using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Prometheus;
using Serilog;
using Zip.Api.CustomerProfile.Configuration;
using Zip.Api.CustomerProfile.Extensions;
using Zip.Api.CustomerProfile.HealthChecks;
using Zip.Api.CustomerProfile.Infrastructure.Metric;
using Zip.Api.CustomerProfile.Interfaces;
using Zip.Api.CustomerProfile.Middleware;
using Zip.Api.CustomerProfile.Services;
using Zip.Core.Prometheus;
using Zip.Core.Serilog;
using Zip.CustomerAcquisition.Core.Kafka.MessageBus;
using Zip.CustomerAcquisition.Core.Kafka.MessageBus.HealthChecks;
using Zip.CustomerProfile.Data;
using Zip.CustomerProfile.Data.ElasticSearch;
using Zip.CustomerProfile.Data.Extensions;
using Zip.CustomerProfile.Data.Interfaces;
using Zip.CustomerProfile.Data.Postgres;

namespace Zip.Api.CustomerProfile
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSerilogLogContextWithCorrelationId();

            app.UseDefaultHealthChecks();
            app.UseReadinessHealthChecks();
            app.UseDiagnosticsHealthChecks();

            app.UseMetricServer();
            app.UseHttpMetrics(options =>
            {
                options.RequestDuration.Histogram = MetricsHelper.GetHistogram("customer_profile_api_http_request",
                    "Customer Profile API Requests", "controller", "action", "method", "code");
            });

            app.UseMiddleware<SerilogLogContextMiddleware>();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer Profile API V1"); });

            app.UseErrorHandlingMiddleware();
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseMerchantMetricReporting();

            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseGraphQL();
            app.UseWarmerService();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // var logger = Log.ForContext<Startup>();
            services.Configure<ApplicationOptions>(Configuration.GetSection("Application"));
            services.AddSerilogLogContextWithCorrelationId();
            services.AddControllers().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var startingAssembly = typeof(Program).Assembly;
            services.Configure<KafkaOptions>(Configuration.GetSection("KafkaOptions"));
            services.AddSingleton(sp =>
            {
                var kafkaOptions = sp.GetRequiredService<IOptions<KafkaOptions>>();
                var busBuilder = new BusBuilder();
                var logger = Log.ForContext<IBus>();

                busBuilder
                    .WithBootstrapServers(kafkaOptions.Value.BootstrapServers)
                    .WithSchemaRegistryUrl(kafkaOptions.Value.SchemaRegistryUrl)
                    .WithHandlersFromAssemblies(startingAssembly)
                    .WithLogger(logger)
                    .WithServiceProvider(sp);

                return busBuilder.BuildAndStart();
            });

            // Register the customer profile service as a concrete service
            // to avoid having the IoC container from having a recursive dependency loop
            services.AddScoped<CustomerProfileService, CustomerProfileService>();
            services.AddScoped<ICustomerProfileService>(provider =>
                new LoggedCustomerProfileService(provider.GetService<CustomerProfileService>(),
                    provider.GetService<ILogger<CustomerProfileService>>()));
            services.AddScoped<IWarmingService, WarmingService>();
            services.AddSingleton<IServiceWarmupState, ServiceWarmupState>();
            services.AddDataAccessServices(Configuration);
            services.AddScoped<IDataProvider>(c =>
                c.GetService<IDataProviderFactory>().GetDataProvider<PostgresDataProvider>());
            services.AddScoped<IMetricReporter, MetricReporter>();
            services.AddTransient<ICustomerProfileBackgroundService, CustomerProfileBackgroundService>();

            services.AddExternalGraphQl(Configuration);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Zip.Api.CustomerProfile", Version = "v1"});
            });

            services.AddHealthChecks()
                .AddCheck<EsHealthCheck>("elastic-search-health")
                .AddCheck<DbHealthCheck>("customerprofile-db-health")
                .AddCheck<MessageBusHealthCheck>("MessageBus")
                .AddCheck<ReadinessHealthCheck>("readiness-health",
                    tags: new[] {HealthCheckExtensions.DefaultReadinessFlag});
        }
    }
}