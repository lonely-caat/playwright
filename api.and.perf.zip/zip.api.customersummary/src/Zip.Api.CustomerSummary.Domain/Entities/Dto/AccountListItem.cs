using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;

namespace Zip.Api.CustomerSummary.Domain.Entities.Dto
{
    public class AccountListItem
    {
        public long Id { get; set; }

        public long? ConsumerId { get; set; }

        public Guid? PublicConsumerId { get; set; }

        public string ProductClassification { get; set; }

        public DateTime? ActivationDate { get; set; }

        public long? OriginationMerchantId { get; set; }

        public string OriginationMerchantName { get; set; }

        public long? OriginationBranchId { get; set; }

        public string OriginationBranchName { get; set; }

        public string CountryId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CreditProfileStateType? CreditStateType { get; set; }

        public long? MerchantId { get; set; }

        public long? MerchantUserId { get; set; }
    }
}
