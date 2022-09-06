using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Communications.Query.GetEmailsSent
{
    public class GetEmailsSentQueryValidator : AbstractValidator<GetEmailsSentQuery>
    {
        public GetEmailsSentQueryValidator()
        {
            RuleFor(x => x.ConsumerId)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Required Consumer Id.");

            RuleFor(x => x.EmailTypes)
                .NotEmpty()
                .WithMessage("Required Email Type.");
        }
    }
}
