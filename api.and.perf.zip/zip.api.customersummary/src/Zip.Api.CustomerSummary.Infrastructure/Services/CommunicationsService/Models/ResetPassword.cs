namespace Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Models
{
    public class ResetPassword
    {
        public string FirstName { get; set; }

        public string Product { get; set; }

        public string Email { get; set; }

        public long ConsumerId { get; set; }

        public long? AccountId { get; set; }
    }
}