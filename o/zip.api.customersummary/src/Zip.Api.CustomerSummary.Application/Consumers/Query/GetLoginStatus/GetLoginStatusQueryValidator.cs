using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Consumers.Query.GetLoginStatus
{
    public class GetLoginStatusQueryValidator : AbstractValidator<GetLoginStatusQuery>
    {
        public GetLoginStatusQueryValidator()
        {
            RuleFor(x => x.ConsumerEmail)
               .NotEmpty()
               .NotNull()
               .EmailAddress();
        }
    }
}