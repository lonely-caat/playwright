using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;

namespace Zip.Api.CustomerSummary.Application.Payments.Query.GetDefaultPaymentMethod
{
    public class GetDefaultPaymentMethodQuery : IRequest<PaymentMethodDto>
    {
        public long ConsumerId { get; private set; }

        public GetDefaultPaymentMethodQuery(long consumerId)
        {
            ConsumerId = consumerId;
        }

        public GetDefaultPaymentMethodQuery()
        {

        }
    }
}
