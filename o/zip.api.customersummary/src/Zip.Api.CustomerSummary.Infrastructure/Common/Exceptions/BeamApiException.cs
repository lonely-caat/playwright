using System;
using System.Runtime.Serialization;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions
{
    [Serializable]
    public class BeamApiException : Exception
    {
        protected BeamApiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public BeamApiException()
        {
        }

        public BeamApiException(string message) : base(message)
        {
        }

        public BeamApiException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
