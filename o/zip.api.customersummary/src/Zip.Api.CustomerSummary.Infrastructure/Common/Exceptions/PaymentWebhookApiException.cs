﻿using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions
{
    [Serializable]
    public class PaymentWebhookApiException : Exception
    {
        public PaymentWebhookApiException()
        {
        }

        public PaymentWebhookApiException(string message) : base(message)
        {
        }

        public PaymentWebhookApiException(string message, Exception inner) : base(message, inner)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected PaymentWebhookApiException(SerializationInfo info, StreamingContext context) : base(info, context) { }

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
