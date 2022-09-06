using System;
using System.Runtime.Serialization;

namespace Zip.Api.CustomerSummary.Api.Exceptions
{
    [Serializable]
    public class HeaderMissingException : Exception
    {
        protected HeaderMissingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public HeaderMissingException()
        {
        }

        public HeaderMissingException(string message) : base(message)
        {
        }

        public HeaderMissingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
