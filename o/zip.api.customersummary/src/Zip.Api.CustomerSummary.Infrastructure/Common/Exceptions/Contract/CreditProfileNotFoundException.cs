using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract
{
    [Serializable]
    public class CreditProfileNotFoundException : Exception
    {
        public long ConsumerId { get; set; }

        public CreditProfileNotFoundException()
        {
        }

        public CreditProfileNotFoundException(string message) : base(message)
        {
        }

        public CreditProfileNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        public CreditProfileNotFoundException(long consumerId) : base($"Cannot find CreditProfile with consumer id - {consumerId}.")
        {
            ConsumerId = consumerId;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected CreditProfileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
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
