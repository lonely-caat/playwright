namespace Zip.Api.CustomerSummary.Domain.Entities.ApplicationEvent
{
    public class AccountDetailsUpdatedEvent : CustomerDetailsUpdatedEvent
    {
        public long AccountId { get; set; }
    }
}
