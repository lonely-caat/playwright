namespace Zip.Api.CustomerSummary.Domain.Entities.Sms
{
    public class SmsWebHookRequest
    {
        // <summary>
        // Phone number message was sent from
        // </summary>
        public string From { get; set; }

        // <summary>
        // Phone number message is sent to
        // </summary>
        public string To { get; set; }

        // <summary>
        // SMS content
        // </summary>
        public string Content { get; set; }
        
        // <summary>
        // AccountSid of the of the <see cref="SmsProvider"/>
        // </summary>
        public string AccountSid { get; set; }
    }
}
