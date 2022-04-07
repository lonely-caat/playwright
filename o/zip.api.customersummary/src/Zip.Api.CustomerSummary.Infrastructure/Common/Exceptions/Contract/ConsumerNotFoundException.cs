using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract
{
    [Serializable]
    public class ConsumerNotFoundException : Exception
    {
        public long ConsumerId { get; set; }
        
        public ConsumerNotFoundException()
        {
        }

        public ConsumerNotFoundException(string message) : base(message)
        {
        }

        public ConsumerNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        public ConsumerNotFoundException(long consumerId) : base($"Consumer not found with {nameof(ConsumerId)}:{consumerId}.")
        {
            ConsumerId = consumerId;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected ConsumerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
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
