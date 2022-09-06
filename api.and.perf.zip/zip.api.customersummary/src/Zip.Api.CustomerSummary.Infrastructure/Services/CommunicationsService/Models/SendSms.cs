namespace Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Models
{
    public class SendSms
    {
        public string PhoneNumber { get; set; }

        public string Message { get; set; }
        
        public long ConsumerId { get; set; }

    }
}