using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Beam.Query.GetCustomerDetails
{
    public class GetCustomerDetailsQueryValidator : AbstractValidator<GetCustomerDetailsQuery>
    {
        public GetCustomerDetailsQueryValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .WithMessage("Missing CustomerId");
        }
    }
}
