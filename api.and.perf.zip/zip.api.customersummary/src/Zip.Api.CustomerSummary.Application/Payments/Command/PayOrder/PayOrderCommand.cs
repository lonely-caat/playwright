using System;
using MediatR;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Response.PayOrder;

namespace Zip.Api.CustomerSummary.Application.Payments.Command.PayOrder
{
    public class PayOrderCommand : IRequest<PayOrderInnerResponse>
    {
        public long AccountId { get; set; }

        public Guid CustomerId { get; set; }

        public long OrderId { get; set; }

        public decimal Amount { get; set; }
    }
}
