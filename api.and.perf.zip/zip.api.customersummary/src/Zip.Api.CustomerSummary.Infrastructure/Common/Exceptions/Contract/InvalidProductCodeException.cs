using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract
{
    [Serializable]
    public class InvalidProductCodeException : Exception
    {
        public string ProductCode { get; set; }
        
        public InvalidProductCodeException()
        {
        }

        public InvalidProductCodeException(string productCode) : base($"Product code {productCode} is invalid.")
        {
            ProductCode = productCode;
        }

        public InvalidProductCodeException(string message, Exception inner) : base(message, inner)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected InvalidProductCodeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ProductCode = info.GetString(nameof(ProductCode));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }
            
            info.AddValue(nameof(ProductCode), ProductCode);
            
            base.GetObjectData(info, context);
        }
    }
}
