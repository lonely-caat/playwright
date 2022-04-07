using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Communications.Command.SendAccountClosedEmail
{
    public class SendAccountClosedEmailCommandValidator : AbstractValidator<SendAccountClosedEmailCommand>
    {
        public SendAccountClosedEmailCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.AccountNumber)
                .NotEmpty();

            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.Product)
                .NotEmpty();
        }
    }
}
