namespace Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Models
{
    public class TokenServiceProviderResponse
    {
        public string TokenReferenceId { get; set; }

        public string PanReferenceId { get; set; }

        public string TokenRequestorId { get; set; }

        public string TokenRequestorName { get; set; }

        public string TokenType { get; set; }

        public string TokenScore { get; set; }

        public string TokenAssurancelevel { get; set; }

        public string TokenEligibilityDecision { get; set; }
    }
}