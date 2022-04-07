namespace Zip.Api.CustomerSummary.Api.Metric
{
    public interface IMetricReporter
    {
        void Tag(string key, string value);
    }

}