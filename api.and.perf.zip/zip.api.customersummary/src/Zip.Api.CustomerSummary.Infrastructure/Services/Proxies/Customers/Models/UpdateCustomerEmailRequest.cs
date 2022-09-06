namespace Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Customers.Models
{
    public class UpdateCustomerEmailRequest
    {
        public string CustomerId { get; set; }

        public string EmailAddress { get; set; }
    }
}
