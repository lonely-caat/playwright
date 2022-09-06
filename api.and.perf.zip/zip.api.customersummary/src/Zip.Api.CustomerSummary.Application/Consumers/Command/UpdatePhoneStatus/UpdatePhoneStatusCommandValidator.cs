using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Consumers.Command.UpdatePhoneStatus
{
    public class UpdatePhoneStatusCommandValidator : AbstractValidator<UpdatePhoneStatusCommand>
    {
        public UpdatePhoneStatusCommandValidator()
        {
            RuleFor(x => x.ConsumerId)
                .GreaterThan(0);

            RuleFor(x => x.PhoneId)
                .GreaterThan(0);

            RuleFor(x => x.Preferred)
                .NotNull()
                .When(x => x.Deleted == null && x.Active == null);

            RuleFor(x => x.Deleted)
                .NotNull()
                .When(x => x.Preferred == null && x.Active == null);

            RuleFor(x => x.Active)
                .NotNull()
                .When(x => x.Preferred == null && x.Deleted == null);
        }
    }
}
