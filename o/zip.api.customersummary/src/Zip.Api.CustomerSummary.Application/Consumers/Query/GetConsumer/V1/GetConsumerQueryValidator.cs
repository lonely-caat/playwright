using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.V1
{
    public class GetConsumerQueryValidator : AbstractValidator<GetConsumerQuery>
    {
        public GetConsumerQueryValidator()
        {
            RuleFor(x => x.ConsumerId)
            .GreaterThan(0);
        }
    }
}