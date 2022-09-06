using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prometheus;
using Serilog;
using System.Reflection;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Zip.Api.CustomerSummary.Api.Extensions;
using Zip.Api.CustomerSummary.Api.HealthChecks;
using Zip.Api.CustomerSummary.Api.Http;
using Zip.Api.CustomerSummary.Api.Middleware;
using Zip.Api.CustomerSummary.Application.Accounts.Command.CloseAccount;
using Zip.Api.CustomerSummary.Infrastructure;
using Zip.Api.CustomerSummary.Persistence;
using Zip.Core.Prometheus;
using ServicesConfiguration = Zip.Api.CustomerSummary.Infrastructure.ServicesConfiguration;
using System.IO;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Zip.Api.CustomerSummary.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            HostingEnvironment = environment;
        }

        private const string AllowProdOrigins = "_allowProdOrigin";

        private const string AllowAll = "AllowAll";

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment HostingEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddHeaderPropagation(options => { options.HeaderNames.Add("X-Correlation-Id"); })
                    .AddScoped(typeof(ValidateHeadersHandler))
                    .AddCors(op =>
                     {
                         op.AddPolicy(AllowAll,
                                      cp =>
                                      {
                                          cp.AllowAnyHeader()
                                            .AllowAnyMethod()
                                            .AllowAnyOrigin();
                                      });

                         op.AddPolicy(AllowProdOrigins,
                                      cp =>
                                      {
                                          cp.WithOrigins("https://*.zip.co")
                                            .SetIsOriginAllowedToAllowWildcardSubdomains()
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                                      });
                     })
                    .AddApiVersioning(opt =>
                     {
                         opt.ReportApiVersions = true;
                         opt.AssumeDefaultVersionWhenUnspecified = true;
                         opt.DefaultApiVersion = new ApiVersion(1, 0);
                     })
                    .AddSwaggerGen(c =>
                     {
                         c.SwaggerDoc("v1",
                                      new OpenApiInfo
                                      {
                                          Title = "Zip.Api.CustomerSummary",
                                          Version = "v1"
                                      });

                         // Allows Swagger to read XML comments
                         var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                         var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                         c.IncludeXmlComments(xmlPath);
                     });

            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Latest)
                    .AddFluentValidation(fv =>
                     {
                         fv.RegisterValidatorsFromAssemblyContaining<CloseAccountCommandValidator>();
                     })
                    .AddNewtonsoftJson(x =>
                     {
                         x.SerializerSettings.Converters.Add(new StringEnumConverter());
                         x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                         x.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                     });

            var assemblyServiceType = typeof(CloseAccountCommandHandler).GetTypeInfo();
            var assemblyServiceInfo = assemblyServiceType.Assembly;

            services.AddMediatR(assemblyServiceInfo)
                    .AddAutoMapper(assemblyServiceInfo, typeof(ServicesConfiguration).GetTypeInfo().Assembly);

            services.AddHealthChecks(Configuration)
                    .AddAuthorization()
                    .AddJsonWebTokenBearer(Configuration)
                    .AddMemoryCache()
                    .AddSingleton(Configuration)
                    .AddOptionsConfiguration(Configuration);

            AddInfrastructureServices(services);
            AddPersistenceServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMetricServer();
            app.UseHttpMetrics(options =>
            {
                options.RequestDuration.Histogram = MetricsHelper.GetHistogram("customer_summary_api_http_request",
                                                                               "Customer Summary Api Requests",
                                                                               new[] { "controller", "action", "method", "code" });
            });

            app.UsePrometheusExceptionCounter();

            app.UseHealthChecks()
               .UseReadinessHealthChecks()
               .UseDiagnosticsHealthChecks()
               .UseSerilogLogContext();

            app.UseCustomMiddleware(env);

            Log.Information($"Environment is {env.EnvironmentName}");

            if (env.IsEnvironment("dev") || env.IsEnvironment("local") || env.IsEnvironment("test"))
            {
                app.UseCors(AllowAll);
            }

            if (env.IsEnvironment("prod") || env.IsEnvironment("sand"))
            {
                app.UseCors(AllowProdOrigins);
            }

            if (!env.EnvironmentName.Equals("Prod"))
            {
                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Zip.Api.CustomerSummary v1.0");
                });
            }

            ConfigureApplicationMiddleware(app)
               .UseAuthentication()
               .UseHttpsRedirection()
               .UseRouting()
               .UseAuthorization()
               .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }

        protected virtual void AddInfrastructureServices(IServiceCollection services)
        {
            services.AddInfrastructureServices(Configuration, HostingEnvironment);
        }

        protected virtual void AddPersistenceServices(IServiceCollection services)
        {
            services.AddPersistenceServices(Configuration);
        }

        protected virtual IApplicationBuilder ConfigureApplicationMiddleware(IApplicationBuilder app)
        {
            return app;
        }
    }
}