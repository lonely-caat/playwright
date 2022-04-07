using System;
using System.Runtime.Serialization;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions
{
    [Serializable]
    public class MfaApiException : Exception
    {
        protected MfaApiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public MfaApiException()
        {
        }

        public MfaApiException(string message) : base(message)
        {
        }

        public MfaApiException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}