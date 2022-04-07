using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Beam.Query.PollReconciliationReport
{
    public class PollReconciliationReportQueryValidator : AbstractValidator<PollReconciliationReportQuery>
    {
        public PollReconciliationReportQueryValidator()
        {
            RuleFor(x => x.Uuid)
                .NotEmpty()
                .WithMessage("Missing Report's UUID");
        }
    }
}
