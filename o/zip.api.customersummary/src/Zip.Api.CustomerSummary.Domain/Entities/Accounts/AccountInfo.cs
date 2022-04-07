using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Zip.Api.CustomerSummary.Domain.Entities.Products;
using Attribute = Zip.Api.CustomerSummary.Domain.Entities.Consumers.Attribute;

namespace Zip.Api.CustomerSummary.Domain.Entities.Accounts
{
    public class AccountInfo
    {
        public long ConsumerId { get; set; }

        public long AccountId { get; set; }

        public long ApplicationId { get; set; }

        public string AccountHash => AccountId.ToString(CultureInfo.InvariantCulture);

        [JsonConverter(typeof(StringEnumConverter))]
        public ProductClassification? Product { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AccountStatus? AccountStatus { get; set; }

        public IEnumerable<Attribute> Attributes { get; set; }

        public Guid CustomerId { get; set; }

        public long AccountTypeId { get; set; }
    }
}
