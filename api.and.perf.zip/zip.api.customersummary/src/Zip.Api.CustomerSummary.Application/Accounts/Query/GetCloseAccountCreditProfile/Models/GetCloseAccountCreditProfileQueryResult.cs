using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;

namespace Zip.Api.CustomerSummary.Application.Accounts.Query.GetCloseAccountCreditProfile.Models
{
    public class GetCloseAccountCreditProfileQueryResult
    {
        public long CreditProfileId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CreditProfileStateType? CurrentStateType { get; set; }

        public IList<StateTypeDetail> NewStateTypes { get; set; } = new List<StateTypeDetail>();
    }
}
