using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetLatestPaymentMethods;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;

namespace Zip.Api.CustomerSummary.Application.Payments.Query.GetDefaultPaymentMethod
{
    public class GetDefaultPaymentMethodQueryHandler : IRequestHandler<GetDefaultPaymentMethodQuery, PaymentMethodDto>
    {
        private readonly IMediator _mediator;

        public GetDefaultPaymentMethodQueryHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<PaymentMethodDto> Handle(GetDefaultPaymentMethodQuery request, CancellationToken cancellationToken)
        {
            var paymentMethods = await _mediator.Send(new GetLatestPaymentMethodsQuery(request.ConsumerId));

            if (paymentMethods.IsNotEmpty())
            {
                var defaultPaymentMethod = paymentMethods.First(x => x.IsDefault);

                if (defaultPaymentMethod != null)
                {
                    return defaultPaymentMethod;
                }
            }

            return null;
        }
    }
}
