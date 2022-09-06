namespace Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Models
{
    public class SendPayNowLink
    {
        public string PhoneNumber { get; set; }

        public string Message { get; set; }

        public string FirstName { get; set; }

        public string Classification { get; set; }

        public string PayNowUrl { get; set; }
        
        public long ConsumerId { get; set; }
        
        public long? AccountId { get; set; }
    }
}