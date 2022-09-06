using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Zip.Api.CustomerSummary.Api.HealthChecks
{
    [ExcludeFromCodeCoverage]
    public static class Configuration
    {
        public const string DefaultReadinessFlag = "readiness";
        public static IServiceCollection AddHealthChecks(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddHealthChecks()
                    .AddCheck("self", () => HealthCheckResult.Healthy())
                    .AddSqlServer(configuration["dbConnString"])
                    .AddCheck<AccountProxyHealthCheck>("AccountProxySettings")
                    .AddCheck<PaymentServiceProxyHealthCheck>("PaymentsServiceProxy")
                    .AddCheck<AccountSearchHealthCheck>("AccountSearchSettings")
                    .AddCheck<CustomersServiceHealthCheck>("CustomersService")
                    .AddCheck<UserManagementProxyHealthCheck>("UserManagementProxy")
                    .AddCheck<CommunicationsServiceProxyHealthCheck>("CommunicationsServiceProxy")
                    .AddCheck<StatementsApiProxyHealthCheck>("StatementsApiProxy")
                    .AddCheck<MerchantDashboardApiProxyHealthCheck>("MerchantDashboardApiProxy")
                    .AddCheck<CoreApiProxyHealthCheck>("CoreApiProxy")
                    .AddCheck<BeamApiProxyHealthCheck>("BeamApiProxy")
                    .AddCheck<CoreGraphServiceHealthCheck>("CoreGraphProxy")
                    .AddCheck<CustomerCoreApiProxyHealthCheck>("CustomerCoreApiProxy");

            return services;
        }

        public static IApplicationBuilder UseHealthChecks(this IApplicationBuilder app)
        {
            return app.UseHealthChecks(
                "/health",
                new HealthCheckOptions
                {
                    Predicate = check => false,
                    ResponseWriter = async (context, report) =>
                    {
                        var result = JsonConvert.SerializeObject(new
                        {
                            status = report.Status.ToString(),
                            details = report.Entries.Select(
                                x => new
                                {
                                    key = x.Key,
                                    value = Enum.GetName(typeof(HealthStatus), x.Value.Status)
                                })
                        });

                        context.Response.ContentType = MediaTypeNames.Application.Json;

                        await context.Response.WriteAsync(result);
                    }
                });
        }

        public static IApplicationBuilder UseReadinessHealthChecks(this IApplicationBuilder builder)
        {
            builder.UseHealthChecks("/health/readiness",
                                    new HealthCheckOptions
                                    {
                                        Predicate = check => check.Tags.Contains(DefaultReadinessFlag)
                                    });

            return builder;
        }

        public static IApplicationBuilder UseDiagnosticsHealthChecks(this IApplicationBuilder builder)
        {
            builder.UseHealthChecks("/health/diagnostics",
                                    new HealthCheckOptions
                                    {
                                        Predicate = check => true,
                                        AllowCachingResponses = true,
                                        ResponseWriter = async (context, report) =>
                                        {
                                            var result = JsonConvert.SerializeObject(report, new StringEnumConverter());
                                            context.Response.ContentType = MediaTypeNames.Application.Json;
                                            await context.Response.WriteAsync(result);
                                        }
                                    });

            return builder;
        }

    }
}
