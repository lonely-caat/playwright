using System;
using MediatR;
using ZipMoney.Services.Payments.Contract.Payments;

namespace Zip.Api.CustomerSummary.Application.Payments.Command.Refund
{
    public class RefundCommand : IRequest<PaymentRefundResponse>
    {
        public Guid PaymentId { get; set; }

        public RefundCommand(Guid paymentId)
        {
            PaymentId = paymentId;
        }

        public RefundCommand()
        {

        }
    }
}
