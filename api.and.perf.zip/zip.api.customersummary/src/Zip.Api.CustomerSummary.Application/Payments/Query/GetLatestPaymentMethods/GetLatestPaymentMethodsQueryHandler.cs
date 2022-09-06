using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetPaymentMethods;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;

namespace Zip.Api.CustomerSummary.Application.Payments.Query.GetLatestPaymentMethods
{
    public class GetLatestPaymentMethodsQueryHandler : IRequestHandler<GetLatestPaymentMethodsQuery, IEnumerable<PaymentMethodDto>>
    {
        private readonly IMediator _mediator;

        public GetLatestPaymentMethodsQueryHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<IEnumerable<PaymentMethodDto>> Handle(GetLatestPaymentMethodsQuery request, CancellationToken cancellationToken)
        {
            var paymentMethods = await _mediator.Send(new GetPaymentMethodsQuery(request.ConsumerId, true, request.State));
            if (paymentMethods.IsNotEmpty())
            {
                return paymentMethods.GroupBy(x => x.Type).Select(g => g.OrderByDescending(x => x.IsDefault).ThenByDescending(x => x.CreatedTimeStamp).First());
            }

            return null;
        }
    }
}
