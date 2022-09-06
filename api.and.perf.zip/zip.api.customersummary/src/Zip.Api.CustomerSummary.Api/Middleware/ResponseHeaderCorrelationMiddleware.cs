using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using NewRelic.Api.Agent;
using System.Threading.Tasks;

namespace Zip.Api.CustomerSummary.Api.Middleware
{
    [ExcludeFromCodeCoverage]
    public static class ResponseHeaderCorrelationMiddlewareExtensions
    {
        public static IApplicationBuilder UseResponseHeaderCorrelation(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseHeaderCorrelationMiddleware>();
        }
    }

    [ExcludeFromCodeCoverage]
    public class ResponseHeaderCorrelationMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseHeaderCorrelationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        [Trace]
        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.OnStarting(state =>
            {
                var httpContext = (HttpContext)state;

                const string correlationIdHeaderName = "X-Correlation-Id";

                if (!httpContext.Request.Headers.TryGetValue(correlationIdHeaderName, out var correlationIdHeaderValue)) return Task.FromResult(0);

                httpContext.Response.Headers.Add(correlationIdHeaderName, correlationIdHeaderValue);
                return Task.FromResult(0);
            }, context);

            await _next(context);
        }
    }
}