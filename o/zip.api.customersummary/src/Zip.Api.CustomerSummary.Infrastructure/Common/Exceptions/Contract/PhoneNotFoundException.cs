using System;
using System.Runtime.Serialization;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract
{
    [Serializable]
    public class PhoneNotFoundException : Exception
    {
        public PhoneNotFoundException()
        {
        }

        public PhoneNotFoundException(long phoneId) : base($"Phone not found with id {phoneId}.")
        {

        }

        public PhoneNotFoundException(string message) : base(message)
        {
        }

        public PhoneNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PhoneNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
