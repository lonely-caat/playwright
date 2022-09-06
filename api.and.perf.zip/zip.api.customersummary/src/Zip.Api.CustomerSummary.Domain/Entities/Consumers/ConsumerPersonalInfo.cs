namespace Zip.Api.CustomerSummary.Domain.Entities.Consumers
{
    public class ConsumerPersonalInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public DriverLicence DriverLicence { get; set; }
        public Address Address { get; set; }
    }
}