namespace Zip.Api.CustomerSummary.Infrastructure.Configuration
{
    public class KinesisSettings
    {
        public string RoleArn { get; set; }
        
        public string RoleSessionName { get; set; }
        
        public int Duration { get; set; }
        
        public bool Enabled { get; set; }
        
        public string KinesisStreamName { get; set; } = "customer-data";
        
        public string Region { get; set; } = "ap-southeast-2";
        
        public string AccessKeyId { get; set; }
        
        public string SecretAccessKey { get; set; }
    }
}
