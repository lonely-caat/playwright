using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Communications.Command.SendPayNowLink
{
    public class SendPayNowLinkCommandValidator : AbstractValidator<SendPayNowLinkCommand>
    {
        public SendPayNowLinkCommandValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0);

            RuleFor(x => x.ConsumerId)
                .GreaterThan(0);
        }
    }
}
