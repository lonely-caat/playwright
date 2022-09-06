namespace Zip.Api.CustomerSummary.Domain.Entities.Consumers
{
    public class ConsumerIdentityInfo
    {
        public long Id { get; set; }

        public IdentityInfoType IdentityInfoType { get; set; }

        public string DisplayValue { get; set; }

        public string FullText => $"{IdentityInfoType.ToString()} is {DisplayValue}";
    }
}
