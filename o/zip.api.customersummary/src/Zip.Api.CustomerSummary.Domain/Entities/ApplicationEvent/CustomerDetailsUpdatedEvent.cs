using System;

namespace Zip.Api.CustomerSummary.Domain.Entities.ApplicationEvent
{
    public class CustomerDetailsUpdatedEvent : Event
    {
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public long ApplicationId { get; set; }
        public Guid? CustomerId { get; set; }
        public string PostCode { get; set; }
        public string Suburb { get; set; }
        public string StateCode { get; set; }
        public string CountryCode { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool? IsActivePhoneNumber { get; set; }
        public CustomerDetailsUpdatedEventSource? Source { get; set; }

        public enum CustomerDetailsUpdatedEventSource
        {
            Wallet,
            Admin
        }
    }
}
