using System.Collections.Generic;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;

namespace Zip.Api.CustomerSummary.Application.Payments.Query.GetLatestPaymentMethods
{
    public class GetLatestPaymentMethodsQuery : IRequest<IEnumerable<PaymentMethodDto>>
    {
        public long ConsumerId { get; set; }
        public string State { get; set; }

        public GetLatestPaymentMethodsQuery(long consumerId, string state)
        {
            ConsumerId = consumerId;
            State = state;
        }

        public GetLatestPaymentMethodsQuery(long consumerId) : this(consumerId, "Approved")
        {

        }

        public GetLatestPaymentMethodsQuery()
        {

        }
    }
}
