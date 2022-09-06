using System.Diagnostics.CodeAnalysis;

namespace Zip.Api.CustomerSummary.Api.Metric
{
    [ExcludeFromCodeCoverage]
    public class MetricReporter : IMetricReporter
    {
        public void Tag(string key, string value)
        {
            NewRelic.Api.Agent.NewRelic.AddCustomParameter(key, value);
        }
    }
}