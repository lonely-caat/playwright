using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Beam.Query.GetRewardActivity
{
    public class GetRewardActivityQueryValidator : AbstractValidator<GetRewardActivityQuery>
    {
        public GetRewardActivityQueryValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .WithMessage("Missing CustomerId");

            RuleFor(x => x.PageNumber)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Invalid PageNumber");

            RuleFor(x => x.PageSize)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Invalid PageSize");
        }
    }
}
