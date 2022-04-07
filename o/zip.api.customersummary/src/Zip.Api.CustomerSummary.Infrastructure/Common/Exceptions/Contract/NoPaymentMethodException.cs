using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract
{
    [Serializable]
    public class PaymentMethodNotFoundException : Exception
    {
        public long ConsumerId { get; set; }

        public PaymentMethodNotFoundException()
        {
        }
        
        public PaymentMethodNotFoundException(string message) : base(message)
        {
        }

        public PaymentMethodNotFoundException(long consumerId) : base($"No payment method found for {nameof(ConsumerId)} is {consumerId}.")
        {
            ConsumerId = consumerId;
        }

        public PaymentMethodNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected PaymentMethodNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ConsumerId = info.GetInt64(nameof(ConsumerId));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }
            
            info.AddValue(nameof(ConsumerId), ConsumerId);
            
            base.GetObjectData(info, context);
        }
    }
}
