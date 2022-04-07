using Microsoft.AspNetCore.Builder;

namespace Zip.Api.CustomerSummary.Api.Middleware
{
    public static class PrometheusExceptionCounterMiddlewareExtension
    {
        public static IApplicationBuilder UsePrometheusExceptionCounter(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PrometheusExceptionCounterMiddleware>();
        }
    }
}
