using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Zip.Core.Extensions;

namespace Zip.Api.CustomerProfile.Extensions
{
    public static class HealthCheckExtensions
    {
        public const string DefaultReadinessFlag = "readiness";

        public static IApplicationBuilder UseDefaultHealthChecks(this IApplicationBuilder builder)
        {
            builder.UseHealthChecks("/health",
                new HealthCheckOptions
                {
                    Predicate = check => false
                });

            return builder;
        }

        public static IApplicationBuilder UseDiagnosticsHealthChecks(this IApplicationBuilder builder)
        {
            builder.UseHealthChecks("/health/diagnostics", new HealthCheckOptions
            {
                ResponseWriter = (httpContext, report) =>
                {
                    httpContext.Response.ContentType = "application/json";
                    return httpContext.Response.WriteAsync(report.ToJObject().ToString(Formatting.Indented));
                }
            });

            return builder;
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
    }
}