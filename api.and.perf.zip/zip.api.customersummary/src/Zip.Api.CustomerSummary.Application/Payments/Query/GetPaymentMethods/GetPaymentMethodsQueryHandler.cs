using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Payments;

namespace Zip.Api.CustomerSummary.Application.Payments.Query.GetPaymentMethods
{
    public class GetPaymentMethodsQueryHandler : IRequestHandler<GetPaymentMethodsQuery, IEnumerable<PaymentMethodDto>>
    {
        private readonly IPaymentsServiceProxy _paymentsServiceProxy;
        private readonly IMapper _mapper;

        public GetPaymentMethodsQueryHandler(IPaymentsServiceProxy paymentsServiceProxy, IMapper mapper)
        {
            _paymentsServiceProxy = paymentsServiceProxy ?? throw new ArgumentNullException(nameof(paymentsServiceProxy));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<IEnumerable<PaymentMethodDto>> Handle(GetPaymentMethodsQuery request, CancellationToken cancellationToken)
        {
            var paymentMethodResponse = await _paymentsServiceProxy.GetAllPaymentMethods($"{request.ConsumerId}", request.IncludeFailedAttempted);
            var paymentMethods = _mapper.Map<IEnumerable<PaymentMethodDto>>(paymentMethodResponse);

            return FilterBy(paymentMethods, request.State);
        }

        private IEnumerable<PaymentMethodDto> FilterBy(IEnumerable<PaymentMethodDto> paymentMethods, string state)
        {
            if (!string.IsNullOrEmpty(state) && paymentMethods.IsNotEmpty())
            {
                return paymentMethods.Where(x => x.StateString.Equals(state, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return paymentMethods;
        }
    }
}
