using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract
{
    [Serializable]
    public class InvalidAccountStatusException : Exception
    {
        public long AccountId { get; set; }
        
        public InvalidAccountStatusException()
        {
        }

        public InvalidAccountStatusException(string message) : base(message)
        {
        }

        public InvalidAccountStatusException(string message, Exception inner) : base(message, inner)
        {
        }

        public InvalidAccountStatusException(long accountId) : base($"Account - {accountId} is not active or operational.")
        {
            AccountId = accountId;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected InvalidAccountStatusException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            AccountId = info.GetInt64(nameof(AccountId));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }
            
            info.AddValue(nameof(AccountId), AccountId);
            
            base.GetObjectData(info, context);
        }
    }
}
