using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract
{
    [Serializable]
    public class AccountTypeNotFoundException : Exception
    {
        public long AccountId { get; set; }

        public AccountTypeNotFoundException()
        {
        }

        public AccountTypeNotFoundException(string message) : base(message)
        {
        }

        public AccountTypeNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        public AccountTypeNotFoundException(long accountId) : base($"Account Type not found with {nameof(AccountId)}:{accountId}.")
        {
            AccountId = accountId;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected AccountTypeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
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
