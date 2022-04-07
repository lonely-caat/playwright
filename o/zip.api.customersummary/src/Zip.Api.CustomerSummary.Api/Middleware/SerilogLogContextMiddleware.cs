using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NewRelic.Api.Agent;
using Serilog.Context;
using Serilog.Core;
using Serilog.Core.Enrichers;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Zip.Api.CustomerSummary.Api.Middleware
{
    [ExcludeFromCodeCoverage]
    public class SerilogLogContextMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly SerilogLogContextMiddlewareOptions _options;

        private readonly Func<HttpContext, List<ILogEventEnricher>> _defaultEnrichers = ctx => new List<ILogEventEnricher>
        {
            new PropertyEnricher("TraceIdentifier", ctx.TraceIdentifier),
            new PropertyEnricher("IpAddress", ctx.Connection.RemoteIpAddress?.ToString()),
            new PropertyEnricher("Host", ctx.Request.Host.ToString()),
            new PropertyEnricher("Path", ctx.Request.Path.ToString()),
            new PropertyEnricher("IsHttps", ctx.Request.IsHttps),
            new PropertyEnricher("Scheme", ctx.Request.Scheme),
            new PropertyEnricher("Method", ctx.Request.Method),
            new PropertyEnricher("ContentType", ctx.Request.ContentType),
            new PropertyEnricher("Protocol", ctx.Request.Protocol),
            new PropertyEnricher("QueryString", ctx.Request.QueryString.ToString()),
            new PropertyEnricher("Query", ctx.Request.Query.ToDictionary(x => x.Key, y => y.Value.ToString())),
            new PropertyEnricher(
                "Headers",
                ctx.Request.Headers.ToDictionary(x => x.Key, y => y.Value.ToString())),
            new PropertyEnricher("Cookies", ctx.Request.Cookies.ToDictionary(
                x => x.Key,
                y => y.Value.ToString(CultureInfo.InvariantCulture))),
        };

        public SerilogLogContextMiddleware(RequestDelegate next, IOptions<SerilogLogContextMiddlewareOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _next = next ?? throw new ArgumentNullException(nameof(next));
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
        }

        [Trace]
        public async Task InvokeAsync(HttpContext context)
        {
            var enrichers = _defaultEnrichers(context);
            if (_options.EnrichersForContextFactory != null)
            {
                try
                {
                    enrichers.AddRange(_options.EnrichersForContextFactory(context));
                }
                catch
                {
                    if (_options.ReThrowEnricherFactoryExceptions)
                    {
                        throw;
                    }
                }
            }

            var nextExecuted = false;
            if (enrichers != null)
            {
                var array = enrichers.ToArray();
                if (array.Any())
                {
                    using (LogContext.Push(array))
                    {
                        await _next(context);
                        nextExecuted = true;
                    }
                }
            }

            if (!nextExecuted)
            {
                await _next(context);
            }
        }
    }
}