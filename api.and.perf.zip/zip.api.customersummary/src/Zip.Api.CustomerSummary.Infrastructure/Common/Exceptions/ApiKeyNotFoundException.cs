using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions
{
    [Serializable]
    public class ApiKeyNotFoundException : Exception
    {
        public ApiKeyNotFoundException()
        {
        }

        public ApiKeyNotFoundException(string message) : base(message)
        {
        }

        public ApiKeyNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected ApiKeyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }
            
            base.GetObjectData(info, context);
        }
    }
}
