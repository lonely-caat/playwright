using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Zip.Api.CustomerSummary.Api.Middleware
{
    [ExcludeFromCodeCoverage]
    public class SerilogLogContextMiddlewareOptions : IOptions<SerilogLogContextMiddlewareOptions>
    {
        public SerilogLogContextMiddlewareOptions Value => this;

        public Func<HttpContext, IEnumerable<ILogEventEnricher>> EnrichersForContextFactory { get; set; }

        public bool ReThrowEnricherFactoryExceptions { get; set; } = true;
    }
}