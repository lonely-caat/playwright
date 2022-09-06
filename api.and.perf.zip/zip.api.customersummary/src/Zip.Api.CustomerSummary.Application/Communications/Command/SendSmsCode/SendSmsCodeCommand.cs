using MediatR;

namespace Zip.Api.CustomerSummary.Application.Communications.Command.SendSmsCode
{
    public class SendSmsCodeCommand : IRequest<string>
    {
        public long ConsumerId { get; set; }

        public SendSmsCodeCommand()
        {

        }

        public SendSmsCodeCommand(long consumerId)
        {
            ConsumerId = consumerId;
        }
    }
}
