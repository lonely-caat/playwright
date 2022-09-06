using System;
using System.Runtime.Serialization;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions
{
    [Serializable]
    public class CoreGraphException : Exception
    {
        protected CoreGraphException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public CoreGraphException()
        {
        }

        public CoreGraphException(string message) : base(message)
        {
        }

        public CoreGraphException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
