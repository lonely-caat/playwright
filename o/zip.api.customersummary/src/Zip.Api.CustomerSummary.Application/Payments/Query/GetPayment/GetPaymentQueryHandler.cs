using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Payments;

namespace Zip.Api.CustomerSummary.Application.Payments.Query.GetPayment
{
    public class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery, PaymentDto>
    {
        private readonly IPaymentsServiceProxy _paymentsServiceProxy;
        private readonly IMapper _mapper;

        public GetPaymentQueryHandler(IPaymentsServiceProxy paymentsServiceProxy, IMapper mapper)
        {
            _paymentsServiceProxy = paymentsServiceProxy ?? throw new ArgumentNullException(nameof(paymentsServiceProxy));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PaymentDto> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
        {
            var paymentResponse = await _paymentsServiceProxy.GetPayment(request.Id);
            return _mapper.Map<PaymentDto>(paymentResponse);
        }

        public Task Handle(GetPaymentQuery getPaymentQuery, object none)
        {
            throw new NotImplementedException();
        }
    }
}
