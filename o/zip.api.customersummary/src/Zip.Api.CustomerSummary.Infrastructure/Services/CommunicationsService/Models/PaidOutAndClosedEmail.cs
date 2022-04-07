namespace Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Models
{
    public class PaidOutAndClosedEmail
    {
        public string FullName { get; set; }

        public string Address { get; set; }

        public long AccountId { get; set; }

        public long ConsumerId { get; set; }

        public string DateOfClosure { get; set; }

        public string Product { get; set; }

        public string DateOfLetterGeneration { get; set; }

        public string Email { get; set; }
    }
}