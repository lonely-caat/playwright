using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Payments;
using ZipMoney.Services.Payments.Contract.Payments;

namespace Zip.Api.CustomerSummary.Application.Payments.Command.Refund
{
    public class RefundCommandHandler : IRequestHandler<RefundCommand, PaymentRefundResponse>
    {
        private readonly IPaymentsServiceProxy _paymentsServiceProxy;

        public RefundCommandHandler(IPaymentsServiceProxy paymentsServiceProxy)
        {
            _paymentsServiceProxy = paymentsServiceProxy ?? throw new ArgumentNullException(nameof(paymentsServiceProxy));
        }

        public async Task<PaymentRefundResponse> Handle(RefundCommand request, CancellationToken cancellationToken)
        {
            return await _paymentsServiceProxy.RefundPayment(request.PaymentId);
        }
    }
}
