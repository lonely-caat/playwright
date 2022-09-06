using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateContact
{
    public class UpdateContactCommandValidator : AbstractValidator<UpdateContactCommand>
    {
        public UpdateContactCommandValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.ConsumerId)
                .GreaterThan(0);

            RuleFor(x => x.Email)
                .NotEmpty()
                .When(source => string.IsNullOrEmpty(source.Mobile));

            RuleFor(x => x.Mobile)
                .NotEmpty()
                .When(source => string.IsNullOrEmpty(source.Email));
        }
    }
}
