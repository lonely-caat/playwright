using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract
{
    [Serializable]
    public class LmsAccountNotFoundException : Exception
    {
        public long AccountId { get; set; }

        public LmsAccountNotFoundException()
        {
        }

        public LmsAccountNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        public LmsAccountNotFoundException(long accountId) : base($"Lms Account not found with {nameof(AccountId)} - {accountId}.")
        {
            AccountId = accountId;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected LmsAccountNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
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
