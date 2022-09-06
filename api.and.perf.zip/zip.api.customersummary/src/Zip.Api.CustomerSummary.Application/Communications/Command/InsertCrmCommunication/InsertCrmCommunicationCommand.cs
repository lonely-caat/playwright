using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.MessageLog;

namespace Zip.Api.CustomerSummary.Application.Communications.Command.InsertCrmCommunication
{
    public class InsertCrmCommunicationCommand : IRequest<bool>
    {
        public long ReferenceId { get; set; }

        public string Subject { get; set; }

        public string Detail { get; set; }

        public MessageLogCategory Category { get; set; }

        public MessageLogDeliveryMethod DeliveryMethod { get; set; }

        public MessageLogType Type { get; set; }

        public MessageLogStatus Status { get; set; }

        public string MessageBody { get; set; } = null;

        public InsertCrmCommunicationCommand()
        {

        }
    }
}