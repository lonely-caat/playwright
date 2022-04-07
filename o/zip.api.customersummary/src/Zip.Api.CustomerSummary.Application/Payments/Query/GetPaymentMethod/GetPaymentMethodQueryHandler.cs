using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Payments;

namespace Zip.Api.CustomerSummary.Application.Payments.Query.GetPaymentMethod
{
    public class GetPaymentMethodQueryHandler : IRequestHandler<GetPaymentMethodQuery, PaymentMethodDto>
    {
        private readonly IPaymentsServiceProxy _paymentsServiceProxy;
        private readonly IMapper _mapper;

        public GetPaymentMethodQueryHandler(IPaymentsServiceProxy paymentsServiceProxy, IMapper mapper)
        {
            _paymentsServiceProxy = paymentsServiceProxy ?? throw new ArgumentNullException(nameof(paymentsServiceProxy));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PaymentMethodDto> Handle(GetPaymentMethodQuery request, CancellationToken cancellationToken)
        {
            var paymentMethodResponse = await _paymentsServiceProxy.GetPaymentMethod(request.Id);
            return _mapper.Map<PaymentMethodDto>(paymentMethodResponse);
        }
    }
}
