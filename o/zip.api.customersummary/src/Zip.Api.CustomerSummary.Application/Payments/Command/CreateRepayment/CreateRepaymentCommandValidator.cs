using System;
using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Payments.Command.CreateRepayment
{
    public class CreateRepaymentCommandValidator : AbstractValidator<CreateRepaymentCommand>
    {
        public CreateRepaymentCommandValidator()
        {
            RuleFor(x => x.AccountId)
                .GreaterThan(0);

            RuleFor(x => x.Amount)
               .GreaterThanOrEqualTo(1);

            RuleFor(x => x.StartDate)
                .Must(x => x.Date > DateTime.Now.Date)
                .WithMessage($"The StartDate must be 1 day later than today - {DateTime.Now:dd/MM/yyyy}.");

            RuleFor(x => x.Frequency)
                .NotNull();
        }
    }
}
