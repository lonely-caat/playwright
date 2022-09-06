using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Communications.Command.SendResetPasswordEmailNew
{
    public class SendResetPasswordEmailNewCommandValidator : AbstractValidator<SendResetPasswordEmailNewCommand>
    {
        public SendResetPasswordEmailNewCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is invalid.");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First name is empty.");

            RuleFor(x => x.Classification)
                .NotNull()
                .WithMessage("Classification is empty.");
        }
    }
}
