using MediatR;

namespace Zip.Api.CustomerSummary.Application.Communications.Command.SendPayNowLink
{
    public class SendPayNowLinkCommand : IRequest
    {
        public long ConsumerId { get; set; }

        public decimal Amount { get; set; }

        public SendPayNowLinkCommand(long consumerId, decimal amount)
        {
            ConsumerId = consumerId;
            Amount = amount;
        }

        public SendPayNowLinkCommand()
        {
        }
    }
}
