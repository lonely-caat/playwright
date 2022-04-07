namespace Zip.Api.CustomerSummary.Infrastructure.Configuration
{
    public class KafkaOptions
    {
        public string BootstrapServers { get; set; }

        public string SchemaRegistryUrl { get; set; }

        public string ConsumerGroupId { get; set; }
    }
}
