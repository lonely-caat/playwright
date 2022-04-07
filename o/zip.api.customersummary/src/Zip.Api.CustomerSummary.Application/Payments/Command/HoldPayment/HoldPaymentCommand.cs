using System;
using MediatR;

namespace Zip.Api.CustomerSummary.Application.Payments.Command.HoldPayment
{
    public class HoldPaymentCommand : IRequest
    {
        public long AccountId { get; set; }

        public DateTime HoldDate { get; set; }
    }
}
