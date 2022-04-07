using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;

namespace Zip.Api.CustomerSummary.Application.Accounts.Query.GetCloseAccountCreditProfile.Models
{
    public class StateTypeDetail
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public CreditProfileStateType StateType { get; set; }

        public IEnumerable<ProfileClassification> Classifications { get; set; }

        public IEnumerable<ProfileAttribute> Attributes { get; set; }
    }
}