using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateLoginStatus
{
    public class UpdateLoginStatusCommandValidator : AbstractValidator<UpdateLoginStatusCommand>
    {
        public UpdateLoginStatusCommandValidator()
        {
            RuleFor(x => x.ConsumerId)
               .GreaterThan(0);

            RuleFor(x => x.LoginStatusType)
               .IsInEnum();
        }
    }
}