namespace Zip.Api.CustomerProfile.Infrastructure.Metric
{
    public class MetricReporter : IMetricReporter
    {
        public void Tag(string key, string value)
        {
            NewRelic.Api.Agent.NewRelic.AddCustomParameter(key, value);
        }
    }
}