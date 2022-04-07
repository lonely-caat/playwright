using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Communications.Command.InsertCrmCommunication
{
    public class InsertCrmCommunicationCommandValidator : AbstractValidator<InsertCrmCommunicationCommand>
    {
        public InsertCrmCommunicationCommandValidator()
        {
            RuleFor(x => x.ReferenceId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Subject)
                .NotEmpty();

            RuleFor(x => x.Detail)
                .NotNull();

            RuleFor(x => x.Category)
                .IsInEnum();

            RuleFor(x => x.DeliveryMethod)
                .IsInEnum();

            RuleFor(x => x.Type)
                .IsInEnum();

            RuleFor(x => x.Status)
                .IsInEnum();
        }
    }
}