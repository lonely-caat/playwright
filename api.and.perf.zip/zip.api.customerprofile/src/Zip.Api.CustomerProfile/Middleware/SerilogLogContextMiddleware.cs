using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NewRelic.Api.Agent;
using Serilog.Context;
using Serilog.Core;
using Serilog.Core.Enrichers;

namespace Zip.Api.CustomerProfile.Middleware
{
    public class SerilogLogContextMiddleware
    {
        private static readonly Func<HttpContext, ILogEventEnricher> HttpContextEnricher = ctx =>
        {
            return new PropertyEnricher("HttpContext", new
            {
                ctx.TraceIdentifier,
                IpAddress = ctx.Connection.RemoteIpAddress?.ToString(),
                ctx.Request.Protocol,
                ctx.Request.Scheme,
                Host = ctx.Request.Host.ToString(),
                Path = ctx.Request.Path.ToString(),
                ctx.Request.Method,
                QueryString = ctx.Request.QueryString.ToString(),
                ctx.Request.ContentType,
                Headers = ctx.Request.Headers.ToDictionary(x => x.Key, y => y.Value.ToString())
            }, true);
        };

        private static readonly Func<IAgent, ILogEventEnricher> NewRelicAgentEnricher = agent =>
            new PropertyEnricher("NewRelic", new
            {
                agent.TraceMetadata.TraceId
            }, true);

        private readonly RequestDelegate _next;

        public SerilogLogContextMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (LogContext.Push(HttpContextEnricher(context),
                NewRelicAgentEnricher(NewRelic.Api.Agent.NewRelic.GetAgent())))
            {
                await _next(context);
            }
        }
    }
}