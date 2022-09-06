using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using Zip.Api.CustomerSummary.Api.Middleware;

namespace Zip.Api.CustomerSummary.Api.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder, IWebHostEnvironment environment)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.UseMiddleware<ExceptionHandlerMiddleware>();

            return builder;
        }
    }
}
