using System;
using System.Net.Http;
using Polly;
using Polly.Bulkhead;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;
using Polly.Retry;
using Polly.Timeout;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.ResiliencePolicies
{
    public static class Policies
    {
        public static AsyncTimeoutPolicy<HttpResponseMessage> ShortTimeout { get; set; } = Policy.TimeoutAsync<HttpResponseMessage>(
              TimeSpan.FromSeconds(10));

        public static AsyncTimeoutPolicy<HttpResponseMessage> LongTimeout { get; set; } = Policy.TimeoutAsync<HttpResponseMessage>(
              TimeSpan.FromSeconds(30));

        public static AsyncBulkheadPolicy<HttpResponseMessage> Bulkhead { get; set; } = Policy.BulkheadAsync<HttpResponseMessage>(12);

        public static AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            var retry = HttpPolicyExtensions.HandleTransientHttpError()
                .WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(500));

            return retry;
        }

        public static AsyncCircuitBreakerPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions.HandleTransientHttpError()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(20));
        }
    }
}
