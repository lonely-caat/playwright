using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Zip.Api.CustomerSummary.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class HeaderPropagationExtensions
    {
        public static IServiceCollection AddHeaderPropagation(this IServiceCollection services, Action<HeaderPropagationOptions> configure)
        {
            services.AddHttpContextAccessor();
            services.Configure(configure);
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IHttpMessageHandlerBuilderFilter, HeaderPropagationMessageHandlerBuilderFilter>());
            return services;
        }

        public static IHttpClientBuilder AddHeaderPropagation(this IHttpClientBuilder builder, Action<HeaderPropagationOptions> configure)
        {
            builder.Services.AddHttpContextAccessor();
            builder.Services.Configure(configure);
            builder.AddHttpMessageHandler(sp =>
            {
                var options = sp.GetRequiredService<IOptions<HeaderPropagationOptions>>();
                var contextAccessor = sp.GetRequiredService<IHttpContextAccessor>();

                return new HeaderPropagationMessageHandler(options.Value, contextAccessor);
            });

            return builder;
        }
    }

    [ExcludeFromCodeCoverage]
    public class HeaderPropagationMessageHandlerBuilderFilter : IHttpMessageHandlerBuilderFilter
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HeaderPropagationOptions _options;

        public HeaderPropagationMessageHandlerBuilderFilter(IOptions<HeaderPropagationOptions> options, IHttpContextAccessor contextAccessor)
        {
            _options = options.Value;
            _contextAccessor = contextAccessor;
        }

        public Action<HttpMessageHandlerBuilder> Configure(Action<HttpMessageHandlerBuilder> next)
        {
            return builder =>
            {
                builder.AdditionalHandlers.Add(new HeaderPropagationMessageHandler(_options, _contextAccessor));
                next(builder);
            };
        }
    }

    [ExcludeFromCodeCoverage]
    public class HeaderPropagationOptions
    {
        public IList<string> HeaderNames { get; } = new List<string>();
    }

    [ExcludeFromCodeCoverage]
    public class HeaderPropagationMessageHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HeaderPropagationOptions _options;

        public HeaderPropagationMessageHandler(HeaderPropagationOptions options, IHttpContextAccessor contextAccessor)
        {
            _options = options;
            _contextAccessor = contextAccessor;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_contextAccessor.HttpContext != null)
            {
                foreach (var headerName in _options.HeaderNames)
                {
                    // Get the incoming header value
                    var headerValue = _contextAccessor.HttpContext.Request.Headers[headerName];
                    if (StringValues.IsNullOrEmpty(headerValue)) continue;

                    request.Headers.TryAddWithoutValidation(headerName, (string[])headerValue);
                }
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}