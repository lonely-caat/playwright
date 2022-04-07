using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class CoreApiException : Exception
    {
        public CoreApiException()
        {
        }
        
        public CoreApiException(string message) : base(message)
        {
        }

        public CoreApiException(string message, Exception inner) : base(message, inner)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected CoreApiException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            base.GetObjectData(info, context);
        }
    }
}
