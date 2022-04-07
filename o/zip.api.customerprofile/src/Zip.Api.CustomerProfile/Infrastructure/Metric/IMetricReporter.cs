namespace Zip.Api.CustomerProfile.Infrastructure.Metric
{
    public interface IMetricReporter
    {
        void Tag(string key, string value);
    }
}