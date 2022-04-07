using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Orders.Query.GetOrderSummary
{
    public class GetOrderSummaryQueryValidator : AbstractValidator<GetOrderSummaryQuery>
    {
        public GetOrderSummaryQueryValidator()
        {
            RuleFor(x => x.OrderId)
               .NotNull()
               .GreaterThan(0);
        }
    }
}