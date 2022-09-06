using MediatR;

namespace Zip.Api.CustomerSummary.Application.Communications.Command.SendPaidOutAndClosedEmail
{
    public class SendPaidOutAndClosedEmailCommand : IRequest<bool>
    {
        public long ConsumerId { get; set; }
    }
}