using System;
using System.Runtime.Serialization;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract
{
    [Serializable]
    public class ConsumerPhoneNotFoundException : Exception
    {
        public ConsumerPhoneNotFoundException()
        {
        }

        public ConsumerPhoneNotFoundException(long consumerId, long phoneId) : base($"ConsumerPhone not found - consumer id {consumerId} and phone id {phoneId}")
        {

        }

        public ConsumerPhoneNotFoundException(string message) : base(message)
        {
        }

        public ConsumerPhoneNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConsumerPhoneNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
