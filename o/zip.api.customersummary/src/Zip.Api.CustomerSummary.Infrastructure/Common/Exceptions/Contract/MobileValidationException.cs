using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract
{
    [Serializable]
    public class MobileValidationException : Exception
    {
        public MobileValidationException()
        {
        }

        public MobileValidationException(string message) : base(message)
        {
        }

        public MobileValidationException(string message, Exception inner) : base(message, inner)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected MobileValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
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
