using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.V2
{
    public class GetConsumerQueryV2Validator : AbstractValidator<GetConsumerQueryV2>
    {
        public GetConsumerQueryV2Validator()
        {
            RuleFor(x => x.ConsumerId)
                .GreaterThan(0);
        }
    }
}