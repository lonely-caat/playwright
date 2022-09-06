using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Zip.Api.CustomerProfile.Infrastructure.Metric;

namespace Zip.Api.CustomerProfile.Middleware
{
    public static class MerchantMetricReportingMiddlewareExtensions
    {
        public static IApplicationBuilder UseMerchantMetricReporting(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MerchantMetricReportingMiddleware>();
        }
    }

    public class MerchantMetricReportingMiddleware
    {
        private const string MerchantIdHeaderKey = "Merchant-Id";
        private readonly RequestDelegate _next;

        public MerchantMetricReportingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IMetricReporter metricReporter)
        {
            var headers = context.Request.Headers;

            if (headers.ContainsKey(MerchantIdHeaderKey))
            {
                headers.TryGetValue(MerchantIdHeaderKey, out var merchantId);
                metricReporter.Tag(MetricKeys.MerchantId, merchantId);
            }

            await _next(context);
        }
    }
}