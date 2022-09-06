using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract
{
    [Serializable]
    public class MobileNotFoundException : Exception
    {
        public MobileNotFoundException()
        {
        }

        public MobileNotFoundException(long consumerId) : base($"Mobile not found for consumer {consumerId}.")
        {

        }

        public MobileNotFoundException(string msg) : base(msg)
        {
        }

        public MobileNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected MobileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
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
