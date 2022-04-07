using System.Collections.Generic;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;

namespace Zip.Api.CustomerSummary.Application.Payments.Query.GetPaymentMethods
{
    public class GetPaymentMethodsQuery : IRequest<IEnumerable<PaymentMethodDto>>
    {
        public long ConsumerId { get; set; }
        public bool IncludeFailedAttempted { get; set; }
        public string State { get; set; }

        public GetPaymentMethodsQuery(long consumerId, bool includeFailedAttempted, string state)
        {
            ConsumerId = consumerId;
            State = state;
            IncludeFailedAttempted = includeFailedAttempted;
        }

        public GetPaymentMethodsQuery()
        {

        }
    }
}
