using System;
using System.Runtime.Serialization;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract
{
    [Serializable]
    public class LockAccountException : Exception
    {
        public LockAccountException()
        {
        }

        public LockAccountException(string message) : base(message)
        {
        }

        public LockAccountException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LockAccountException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
