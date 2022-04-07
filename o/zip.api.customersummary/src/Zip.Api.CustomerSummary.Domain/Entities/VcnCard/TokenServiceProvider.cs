namespace Zip.Api.CustomerSummary.Domain.Entities.VcnCard
{
    public class TokenServiceProvider
    {
        public string TokenReferenceId { get; set; }

        public string PanReferenceId { get; set; }

        public string TokenRequestorId { get; set; }

        public string TokenRequestorName { get; set; }

        public string TokenType { get; set; }

        public string TokenScore { get; set; }

        public string TokenAssuranceLevel { get; set; }

        public string TokenEligibilityDecision { get; set; }
    }
}