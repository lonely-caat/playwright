using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract
{
    [Serializable]
    public class ProductNotFoundException : Exception
    {
        public long ProductId { get; set; }

        public ProductNotFoundException()
        {
        }

        public ProductNotFoundException(string message) : base(message)
        {
        }

        public ProductNotFoundException(long productId) : base($"Product not found with id {productId}.")
        {
            ProductId = productId;
        }

        public ProductNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected ProductNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ProductId = info.GetInt64(nameof(ProductId));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }
            
            info.AddValue(nameof(ProductId), ProductId);
            
            base.GetObjectData(info, context);
        }
    }
}
