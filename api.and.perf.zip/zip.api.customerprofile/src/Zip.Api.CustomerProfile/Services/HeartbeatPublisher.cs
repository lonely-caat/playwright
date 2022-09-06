using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Zip.Api.CustomerProfile.Configuration;
using Zip.Core.Prometheus;

namespace Zip.Api.CustomerProfile.Services
{
    /// <summary>
    ///     Publishes heartbeat periodically with health status of downstream services.
    /// </summary>
    public class HeartbeatPublisher : IHostedService
    {
        private const int _heartbeatFrequencyInMilliseconds = 15000;
        private readonly string _healthCheckUrl;

        public HeartbeatPublisher(IOptions<ApplicationOptions> applicationOptions)
        {
            if (applicationOptions == null) throw new ArgumentNullException(nameof(applicationOptions));

            _healthCheckUrl = $"http://localhost:{applicationOptions.Value.Port}/health/diagnostics";
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(_heartbeatFrequencyInMilliseconds);

                await SendHeartbeat();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async Task SendHeartbeat()
        {
            try
            {
                using (var httpClient = new HttpClient())
                using (var httpResponse = await httpClient.GetAsync(new Uri(_healthCheckUrl)))
                {
                    var content = await httpResponse.Content.ReadAsStringAsync();
                    var (healthState, reason) = GetHealthStatus(content);

                    MetricsHelper.GetCounter("customer_profile_api_health_check", "Customer Profile API Health Check",
                            "healthstate", "failure")
                        .WithLabels(healthState, reason)
                        .Inc();
                }
            }
            catch (Exception ex)
            {
                MetricsHelper.GetCounter("customer_profile_api_health_check", "Customer Profile API Health Check",
                        "healthstate", "failure")
                    .WithLabels("Unhealthy", ex.GetType().Name)
                    .Inc();
            }
        }

        private (string, string) GetHealthStatus(string healthCheckResponse)
        {
            var jsonResponse = JObject.Parse(healthCheckResponse);
            var healthState = (string) jsonResponse["status"];

            foreach (var item in (JObject) jsonResponse["results"])
                if ((string) item.Value["status"] != "Healthy")
                    return (healthState, item.Key);

            return (healthState, string.Empty);
        }
    }
}