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

namespace Zip.Api.CustomerSummary.Application.Payments.Query.GetPayments
{
    public class GetPaymentsQueryHandler : IRequestHandler<GetPaymentsQuery, IEnumerable<PaymentDto>>
    {
        private readonly IPaymentsServiceProxy _paymentsServiceProxy;
        private readonly IMapper _mapper;

        public GetPaymentsQueryHandler(IPaymentsServiceProxy paymentsServiceProxy, IMapper mapper)
        {
            _paymentsServiceProxy = paymentsServiceProxy ?? throw new ArgumentNullException(nameof(paymentsServiceProxy));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<PaymentDto>> Handle(GetPaymentsQuery request, CancellationToken cancellationToken)
        {
            request.FromDate = request.FromDate ?? DateTime.Now.AddMonths(-6).AddDays(-1);
            request.ToDate = request.ToDate ?? DateTime.Now.AddDays(1);
            var paymentResponses = await _paymentsServiceProxy.GetAllPayments(request.AccountId.ToString(), request.FromDate, request.ToDate, request.PaymentBatchId);
            var payments = _mapper.Map<IEnumerable<PaymentDto>>(paymentResponses);

            return payments.IsEmpty() ? null : payments.OrderByDescending(x => x.CreatedDateTimeLocal);
        }
    }
}
