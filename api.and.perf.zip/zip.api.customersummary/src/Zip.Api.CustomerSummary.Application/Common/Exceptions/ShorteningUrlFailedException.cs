using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Zip.Api.CustomerSummary.Application.Common.Exceptions
{
    [Serializable]
    public class ShorteningUrlFailedException : Exception
    {
        public string LongUrl { get; set; }
        
        public ShorteningUrlFailedException()
        {

        }

        public ShorteningUrlFailedException(string longUrl)
        {
            LongUrl = longUrl;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected ShorteningUrlFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            LongUrl = info.GetString(nameof(LongUrl));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }
            info.AddValue(nameof(LongUrl), LongUrl);
            base.GetObjectData(info, context);
        }
    }
}
