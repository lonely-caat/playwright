using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract
{
    [Serializable]
    public class InvalidCountryCodeException : Exception
    {
        public string CountryCode { get; set; }

        public InvalidCountryCodeException()
        {
        }

        public InvalidCountryCodeException(string message, Exception inner) : base(message, inner)
        {
        }

        public InvalidCountryCodeException(string countryCode) : base($"CountryId {countryCode} is invalid.")
        {
            CountryCode = countryCode;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected InvalidCountryCodeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            CountryCode = info.GetString(nameof(CountryCode));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue(nameof(CountryCode), CountryCode);
            
            base.GetObjectData(info, context);
        }
    }
}
