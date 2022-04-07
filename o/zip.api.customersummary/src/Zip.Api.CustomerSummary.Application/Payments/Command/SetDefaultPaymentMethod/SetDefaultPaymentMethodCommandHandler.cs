using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Payments;

namespace Zip.Api.CustomerSummary.Application.Payments.Command.SetDefaultPaymentMethod
{
    public class SetDefaultPaymentMethodCommandHandler : IRequestHandler<SetDefaultPaymentMethodCommand>
    {
        private readonly IPaymentsServiceProxy _paymentsServiceProxy;

        public SetDefaultPaymentMethodCommandHandler(IPaymentsServiceProxy paymentsServiceProxy)
        {
            _paymentsServiceProxy = paymentsServiceProxy ?? throw new ArgumentNullException(nameof(paymentsServiceProxy));
        }

        public async Task<Unit> Handle(SetDefaultPaymentMethodCommand request, CancellationToken cancellationToken)
        {
            await _paymentsServiceProxy.SetDefaultPaymentMethod(request.PaymentMethodId, $"{request.ConsumerId}");

            return Unit.Value;
        }
    }
}
