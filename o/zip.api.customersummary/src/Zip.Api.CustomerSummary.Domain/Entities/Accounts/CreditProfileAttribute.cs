namespace Zip.Api.CustomerSummary.Domain.Entities.Accounts
{
    public class CreditProfileAttribute
    {
        public long Id { get; set; }

        public long ProfileAttributeId { get; set; }

        public long CreditProfileId { get; set; }

        public System.DateTime TimeStamp { get; set; }
    }
}
