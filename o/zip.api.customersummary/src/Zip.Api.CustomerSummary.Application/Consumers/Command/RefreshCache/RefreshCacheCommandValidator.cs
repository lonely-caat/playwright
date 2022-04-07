using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Consumers.Command.RefreshCache
{
    public class RefreshCacheCommandValidator : AbstractValidator<RefreshCacheCommand>
    {
        public RefreshCacheCommandValidator()
        {
            RuleFor(x => x.ConsumerId)
                .GreaterThan(0);
        }
    }
}
