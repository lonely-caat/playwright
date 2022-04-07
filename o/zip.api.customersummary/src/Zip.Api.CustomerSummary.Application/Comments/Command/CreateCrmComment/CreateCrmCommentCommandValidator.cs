using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Comments.Command.CreateCrmComment
{
    public class CreateCrmCommentCommandValidator : AbstractValidator<CreateCrmCommentCommand>
    {
        public CreateCrmCommentCommandValidator()
        {
            RuleFor(x => x.ReferenceId)
                .GreaterThan(0);

            RuleFor(x => x.Detail)
                .NotEmpty();

            RuleFor(x => x.Category)
                .NotNull();

            RuleFor(x => x.Type)
                .NotNull();
        }
    }
}
