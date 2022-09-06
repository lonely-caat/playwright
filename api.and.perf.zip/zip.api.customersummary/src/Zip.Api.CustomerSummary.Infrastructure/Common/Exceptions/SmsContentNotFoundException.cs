using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions
{
    [Serializable]
    public class SmsContentNotFoundException : Exception
    {
        public string Name { get; set; }

        public SmsContentNotFoundException()
        {
        }
        
        public SmsContentNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        public SmsContentNotFoundException(string name) : base($"Sms content not found with {nameof(name)}:{name}.")
        {
            Name = name;
        }
        
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected SmsContentNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Name = info.GetString(nameof(Name));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }
            
            info.AddValue(nameof(Name), Name);
            
            base.GetObjectData(info, context);
        }
    }
}
