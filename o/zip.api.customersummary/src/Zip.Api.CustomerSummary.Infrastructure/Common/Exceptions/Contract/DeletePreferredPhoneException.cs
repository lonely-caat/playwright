using System;
using System.Runtime.Serialization;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract
{
    [Serializable]
    public class DeletePreferredPhoneException : Exception
    {
        public DeletePreferredPhoneException()
        {
        }

        public DeletePreferredPhoneException(string message) : base(message)
        {
        }

        public DeletePreferredPhoneException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DeletePreferredPhoneException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
