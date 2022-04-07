namespace Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Models
{
    public class CloseAccount
    {
        public string FirstName { get; set; }

        public string Product { get; set; }

        public string AccountNumber { get; set; }

        public string Email { get; set; }

        public long ConsumerId { get; set; }
    }
}