using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract
{
    [Serializable]
    public class NonOperationalAccountException : Exception
    {
        public long AccountId { get; set; }
        
        public NonOperationalAccountException()
        {
        }
        
        public NonOperationalAccountException(long accountId) : base($"Not able to generate statement for non - operational account with id {accountId}.")
        {
            AccountId = accountId;
        }

        public NonOperationalAccountException(string message, Exception inner) : base(message, inner)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected NonOperationalAccountException(SerializationInfo info, StreamingContext context) : base(info, context)
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
