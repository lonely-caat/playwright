namespace Zip.Api.CustomerSummary.Domain.Entities.Consumers
{
    public class Merchant
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string URL { get; set; }
        
        public bool Exclusive { get; set; }
    }
}
