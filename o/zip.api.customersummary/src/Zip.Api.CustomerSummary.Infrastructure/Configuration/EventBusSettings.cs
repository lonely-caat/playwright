namespace Zip.Api.CustomerSummary.Infrastructure.Configuration
{
    public class EventBusSettings
    {
        public string ArnPrefix { get; set; }

        public int Duration { get; set; }

        public string RoleArn { get; set; }

        public string RoleSessionName { get; set; }

        public string Region { get; set; }
    }
}
