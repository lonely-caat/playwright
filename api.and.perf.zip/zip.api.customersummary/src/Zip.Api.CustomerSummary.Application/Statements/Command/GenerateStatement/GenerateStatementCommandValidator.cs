using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Statements.Command.GenerateStatement
{
    public class GenerateStatementCommandValidator : AbstractValidator<GenerateStatementCommand>
    {
        public GenerateStatementCommandValidator()
        {
            RuleFor(x => x.AccountId)
                .GreaterThan(0)
                .WithMessage("Invalid account id.");
        }
    }
}
