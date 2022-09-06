using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract
{
    [Serializable]
    public class ContactNotFoundException : Exception
    {
        public long ConsumerId { get; set; }

        public ContactNotFoundException()
        {
        }

        public ContactNotFoundException(string message) : base(message)
        {
        }

        public ContactNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        public ContactNotFoundException(long consumerId) : base($"Consumer not found with {nameof(ConsumerId)}:{consumerId}.")
        {
            ConsumerId = consumerId;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected ContactNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
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
