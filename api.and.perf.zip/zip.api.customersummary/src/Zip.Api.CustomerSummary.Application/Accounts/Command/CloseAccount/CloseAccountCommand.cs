using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;

namespace Zip.Api.CustomerSummary.Application.Accounts.Command.CloseAccount
{
    public class CloseAccountCommand : IRequest
    {
        public long ConsumerId { get; set; }
        public long AccountId { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public CreditProfileStateType? CreditStateType { get; set; }
        public long CreditProfileId { get; set; }
        public long? ProfileAttributeId { get; set; }
        public long? ProfileClassificationId { get; set; }
        public string Comments { get; set; }
        public string ChangedBy { get; set; }
    }
}
