using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Zip.Api.CustomerSummary.Api.Middleware
{
    [ExcludeFromCodeCoverage]
    public static class SerilogLogContextMiddlewareExtensions
    {
        public static IApplicationBuilder UseSerilogLogContext(this IApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            var options = new SerilogLogContextMiddlewareOptions();
            return builder.UseMiddleware<SerilogLogContextMiddleware>(Options.Create(options));
        }
    }
}