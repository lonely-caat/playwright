using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Zip.Api.CustomerSummary.Application.Common.Exceptions
{
    [Serializable]
    public class PayNowUrlGenerationFailedException : Exception
    {
        public string Classification { get; set; }
        public string CountryId { get; set; }
        public decimal Amount { get; set; }
        public long AccountId { get; set; }

        public PayNowUrlGenerationFailedException()
        {

        }

        public PayNowUrlGenerationFailedException(string message) : base(message)
        {
        }

        public PayNowUrlGenerationFailedException(long accountId, decimal amount, string countryId, string classification)
        {
            AccountId = accountId;
            Amount = amount;
            CountryId = countryId;
            Classification = classification;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected PayNowUrlGenerationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Classification = info.GetString(nameof(Classification));
            CountryId = info.GetString(nameof(CountryId));
            Amount = info.GetDecimal(nameof(Amount));
            AccountId = info.GetInt64(nameof(AccountId));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }
            info.AddValue(nameof(Classification), Classification);
            info.AddValue(nameof(Amount), Amount);
            info.AddValue(nameof(CountryId), CountryId);
            info.AddValue(nameof(AccountId), AccountId);
            base.GetObjectData(info, context);
        }
    }
}
