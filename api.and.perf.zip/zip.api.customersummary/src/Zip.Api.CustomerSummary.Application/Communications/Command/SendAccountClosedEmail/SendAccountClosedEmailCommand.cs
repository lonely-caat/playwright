using MediatR;
using Newtonsoft.Json;

namespace Zip.Api.CustomerSummary.Application.Communications.Command.SendAccountClosedEmail
{
    public class SendAccountClosedEmailCommand : IRequest
    {
        public SendAccountClosedEmailCommand()
        {
        }

        [JsonProperty("first-name")]
        public string FirstName { get; set; }

        [JsonProperty("product")]
        public string Product { get; set; }

        [JsonProperty("account-number")]
        public string AccountNumber { get; set; }

        public string Email { get; set; }
    }
}
