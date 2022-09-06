using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract
{
    [Serializable]
    public class MakePaymentFailedException : Exception
    {
        public string Reason { get; set; }

        public MakePaymentFailedException()
        {
        }

        public MakePaymentFailedException(string reason) : base(reason)
        {
            Reason = reason;
        }

        public MakePaymentFailedException(string message, Exception inner) : base(message, inner)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected MakePaymentFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Reason = info.GetString(nameof(Reason));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }
            
            info.AddValue(nameof(Reason), Reason);
            
            base.GetObjectData(info, context);
        }
    }
}
