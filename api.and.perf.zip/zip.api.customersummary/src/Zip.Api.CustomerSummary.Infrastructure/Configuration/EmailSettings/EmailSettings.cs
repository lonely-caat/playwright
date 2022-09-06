namespace Zip.Api.CustomerSummary.Infrastructure.Configuration.EmailSettings
{
    public class EmailSettings
    {
        public string ApiKey { get; set; }
        
        public ResetPasswordEmailTemplate ResetPasswordEmail { get; set; }

        public EmailTemplate CloseAccountEmail { get; set; }
    }
}
