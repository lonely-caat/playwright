using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Orders.Query.GetOrderInstallments
{
    public class GetOrderInstallmentsQueryValidator : AbstractValidator<GetOrderInstallmentsQuery>
    {
        public GetOrderInstallmentsQueryValidator()
        {
            RuleFor(x => x.AccountId)
               .GreaterThan(0)
               .WithMessage("Invalid AccountId");

            RuleFor(x => x.OrderId)
               .GreaterThan(0)
               .WithMessage("Invalid OrderId");
        }
    }
}
