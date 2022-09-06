using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Prometheus;
using Zip.Core.Prometheus;

namespace Zip.Api.CustomerProfile.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly Counter _counter;
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
            _counter = MetricsHelper.GetCounter("customer_profile_api_error_count",
                "Customer Profile API Error Response", "path", "method");
        }

        public async Task Invoke(HttpContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            try
            {
                await _counter.WithLabels(context.Request.Path, context.Request.Method)
                    .CountExceptionsAsync(() => next(context));
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var result = JsonSerializer.Serialize(new {error = ex.Message});
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(result);
        }
    }
}