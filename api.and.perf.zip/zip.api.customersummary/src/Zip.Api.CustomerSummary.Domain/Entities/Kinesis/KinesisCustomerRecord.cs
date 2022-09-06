using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Zip.Api.CustomerSummary.Domain.Entities.Kinesis.Interfaces;

namespace Zip.Api.CustomerSummary.Domain.Entities.Kinesis
{
    public class KinesisCustomerRecord : IKinesisRecord
    {
        public KinesisCustomerRecord()
        {
            Type = KinesisRecordType.Customer;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public KinesisRecordType Type { get; }

        public string Event { get; set; }

        public long ConsumerId { get; set; }

        public string ConsumerAttributes { get; set; }

        public string Email { get; set; }

        public byte EmailOrigination { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Product { get; set; }

        public string FacebookId { get; set; }

        public string PayPalId { get; set; }

        public string LinkedInId { get; set; }

        public string VedaId { get; set; }

        public string PublicConsumerId { get; set; }

        public DateTime TimeStamp { get; set; }

        public AddressDetail BillingAddress { get; set; }

        public AddressDetail ShippingAddress { get; set; }

        public Identification IdentificationDocument { get; set; }

        public IEnumerable<string> GoogleAnalyticsDeviceIds { get; set; }

        public string CardId { get; set; }

        public string CardNumber { get; set; }

        public string CardExpiry { get; set; }

        public string BankId { get; set; }

        public string BankAccountNumber { get; set; }

        public string BankBsb { get; set; }

        public string CreditProfileStateType { get; set; }

        public string AccountStatus { get; set; }
    }
}
