using System.Globalization;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Reflection;
using AutoMapper;
using Zip.Api.CustomerSummary.Api.Extensions;
using Zip.Api.CustomerSummary.Api.Middleware;
using MediatR;
using Zip.Api.CustomerSummary.Application.Accounts.Command.CloseAccount;
using Zip.Api.CustomerSummary.Infrastructure;

namespace Zip.Api.CustomerSummary.Api
{
    public class TestStartup
    {
        public TestStartup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            HostingEnvironment = environment;
        }

        private const string AllowAll = "AllowAll";

        public IConfiguration Configuration { get; }
        
        public IWebHostEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();

            services.AddCors(op =>
                     {
                         op.AddPolicy(
                             AllowAll,
                             cp =>
                             {
                                 cp.AllowAnyHeader()
                                   .AllowAnyMethod()
                                   .AllowAnyOrigin();
                             });
                     })
                    .AddApiVersioning(opt =>
                     {
                         opt.ReportApiVersions = true;
                         opt.AssumeDefaultVersionWhenUnspecified = true;
                         opt.DefaultApiVersion = new ApiVersion(1, 0);
                     })
                    .AddHeaderPropagation(options => { options.HeaderNames.Add("X-Correlation-Id"); });

            services.AddMemoryCache()
                    .AddSingleton(Configuration)
                    .AddAuthorization()
                    .AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Latest)
                    .AddFluentValidation(fv =>
                     {
                         fv.RegisterValidatorsFromAssemblyContaining<CloseAccountCommandValidator>();
                     })
                    .AddNewtonsoftJson(options =>
                     {
                         options.SerializerSettings.Culture = new CultureInfo("en-AU");
                     });
            
            var assemblyServiceType = typeof(CloseAccountCommandHandler).GetTypeInfo();
            var assemblyServiceInfo = assemblyServiceType.Assembly;

            services.AddMediatR(assemblyServiceInfo)
                    .AddAutoMapper(assemblyServiceInfo, typeof(ServicesConfiguration).GetTypeInfo().Assembly)
                    .AddOptionsConfiguration(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Log.Information($"Environment is {env.EnvironmentName}");

            app.UseCustomMiddleware(env)
               .UseCors(AllowAll)
               .UseAuthentication()
               .UseMockAuthorizationMiddleware()
               .UseHttpsRedirection()
               .UseRouting()
               .UseAuthorization()
               .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}