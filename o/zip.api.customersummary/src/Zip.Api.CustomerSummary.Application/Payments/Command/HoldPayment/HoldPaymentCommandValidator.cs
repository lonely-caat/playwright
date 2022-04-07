using System;
using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Payments.Command.HoldPayment
{
    public class HoldPaymentCommandValidator : AbstractValidator<HoldPaymentCommand>
    {
        public HoldPaymentCommandValidator()
        {
            RuleFor(x => x.AccountId)
                .GreaterThan(0);

            RuleFor(x => x.HoldDate)
                .GreaterThan(DateTime.Now);
        }
    }
}
