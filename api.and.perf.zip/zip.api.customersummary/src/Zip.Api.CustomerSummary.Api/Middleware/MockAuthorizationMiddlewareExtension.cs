using System;
using Microsoft.AspNetCore.Builder;

namespace Zip.Api.CustomerSummary.Api.Middleware
{
    public static class Configuration
    {
        public static IApplicationBuilder UseMockAuthorizationMiddleware(this IApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentException(nameof(builder));
            }

            return builder.UseMiddleware<MockAuthorizationMiddleware>();
        }
    }
}
