using Microsoft.AspNetCore.Http;
using Prometheus;
using System;
using System.Threading.Tasks;
using Zip.Core.Prometheus;

namespace Zip.Api.CustomerSummary.Api.Middleware
{
    public class PrometheusExceptionCounterMiddleware
    {
        private const string ExceptionCounterName = "zip_customer_summary_api_exception_count";
        private const string ExceptionCounterDescription = "ZIP Customer Summary API Exception Error Response";
        private readonly RequestDelegate _next;
        private readonly Counter _counter;

        public PrometheusExceptionCounterMiddleware(RequestDelegate next)
        {
            _next = next;
            _counter = MetricsHelper.GetCounter(ExceptionCounterName,
                                                ExceptionCounterDescription,
                                                "path",
                                                "method");
        }

        public async Task Invoke(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            await _counter.WithLabels(
                context.Request.Path,
                context.Request.Method).CountExceptionsAsync(() => _next(context));
        }

    }
}
