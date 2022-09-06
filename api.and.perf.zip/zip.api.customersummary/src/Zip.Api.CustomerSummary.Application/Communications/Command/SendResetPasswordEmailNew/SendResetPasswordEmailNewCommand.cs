using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Zip.Api.CustomerSummary.Domain.Entities.Products;

namespace Zip.Api.CustomerSummary.Application.Communications.Command.SendResetPasswordEmailNew
{
    public class SendResetPasswordEmailNewCommand : IRequest
    {
        public long ConsumerId { get; set; }
        
        public string Email { get; set; }
        
        public string FirstName { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public ProductClassification? Classification { get; set; }

        public long? AccountId { get; set; }

        public SendResetPasswordEmailNewCommand(long consumerId, string email, string firstName, ProductClassification classification, long? accountId)
        {
            ConsumerId = consumerId;
            Email = email;
            FirstName = firstName;
            Classification = classification;
            AccountId = accountId;
        }

        public SendResetPasswordEmailNewCommand()
        {

        }
    }
}
