using System;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;

namespace Zip.Api.CustomerSummary.Application.Payments.Query.GetPaymentMethod
{
    public class GetPaymentMethodQuery : IRequest<PaymentMethodDto>
    {
        public Guid Id { get; set; }

        public GetPaymentMethodQuery(Guid id)
        {
            Id = id;
        }

        public GetPaymentMethodQuery()
        {

        }
    }
}
