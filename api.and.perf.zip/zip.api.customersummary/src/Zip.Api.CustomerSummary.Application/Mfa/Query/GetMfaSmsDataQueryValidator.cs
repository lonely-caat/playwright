using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Mfa.Query
{
    public class GetMfaSmsDataQueryValidator : AbstractValidator<GetMfaSmsDataQuery>
    {
        public GetMfaSmsDataQueryValidator()
        {
            RuleFor(x => x.ConsumerId)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Missing ConsumerId");
        }
    }
}