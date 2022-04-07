using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract
{
    [Serializable]
    public class AccountNotFoundException : Exception
    {
        public long ConsumerId { get; set; }
        
        public long AccountId { get; set; }

        public AccountNotFoundException()
        {
        }

        public AccountNotFoundException(string message) : base(message)
        {
        }

        public AccountNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        public AccountNotFoundException(long consumerId, long accountId) : base(
                $"Account not found with {nameof(ConsumerId)}:{consumerId} or {nameof(AccountId)}:{accountId}.")
        {
            ConsumerId = consumerId;
            AccountId = accountId;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected AccountNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ConsumerId = info.GetInt64(nameof(ConsumerId));
            AccountId = info.GetInt64(nameof(AccountId));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }
            
            info.AddValue(nameof(ConsumerId), ConsumerId);
            info.AddValue(nameof(AccountId), AccountId);
            
            base.GetObjectData(info, context);
        }
    }
}