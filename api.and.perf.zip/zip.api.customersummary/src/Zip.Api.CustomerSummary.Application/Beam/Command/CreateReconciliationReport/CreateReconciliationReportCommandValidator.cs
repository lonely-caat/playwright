using FluentValidation;
using Zip.Api.CustomerSummary.Application.Beam.Command.CreateReconciliationReport;

namespace Zip.Api.CustomerSummary.Application.Beam.Command.CreateBeamReconciliationReport
{
    public class CreateReconciliationReportCommandValidator : AbstractValidator<CreateReconciliationReportCommand>
    {
        public CreateReconciliationReportCommandValidator()
        {
            RuleFor(x => x.SelectedDate)
                .NotNull()
                .WithMessage("Missing SelectedDate");
        }
    }
}
