using System;
using MediatR;

namespace Zip.Api.CustomerSummary.Application.Payments.Command.SetDefaultPaymentMethod
{
    public class SetDefaultPaymentMethodCommand : IRequest
    {
        public Guid PaymentMethodId { get; set; }
        public long ConsumerId { get; set; }

        public SetDefaultPaymentMethodCommand(Guid paymentMethodId, long consumerId)
        {
            PaymentMethodId = paymentMethodId;
            ConsumerId = consumerId;
        }

        public SetDefaultPaymentMethodCommand()
        {

        }
    }
}
