using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;

namespace Zip.Api.CustomerSummary.Application.Payments.Query.GetPayment
{
    public class GetPaymentQuery : IRequest<PaymentDto>
    {
        public string Id { get; set; }

        public GetPaymentQuery(string id)
        {
            Id = id;
        }

        public GetPaymentQuery()
        {

        }
    }
}
