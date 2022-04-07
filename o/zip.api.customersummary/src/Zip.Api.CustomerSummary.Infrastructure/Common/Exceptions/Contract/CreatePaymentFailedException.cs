using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract
{
    [Serializable]
    public class CreatePaymentFailedException : System.Exception
    {
        public CreatePaymentFailedException()
        {
        }

        public CreatePaymentFailedException(string msg) : base(msg)
        {
        }

        public CreatePaymentFailedException(string message, System.Exception inner) : base(message, inner)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected CreatePaymentFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
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
