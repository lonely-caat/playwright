using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions
{
    [Serializable]
    public class StatementDateNotFoundException : Exception
    {
        public long AccountId { get; set; }

        public StatementDateNotFoundException()
        {
        }

        public StatementDateNotFoundException(string message) : base(message)
        {
        }

        public StatementDateNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        public StatementDateNotFoundException(long accountId) : base($"Cannot find StatementDate in Account - {accountId}.")
        {
            AccountId = accountId;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected StatementDateNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
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
