namespace Zip.Api.CustomerSummary.Domain.Entities.Communications
{
    public class SmsResponse
    {
        public bool Success { get; set; } = true;

        public string ErrorMessage { get; set; }
    }
}
