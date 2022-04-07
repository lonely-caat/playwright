using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;

namespace Zip.Api.CustomerSummary.Application.Consumers.Command.UpdatePhoneStatus
{
    public class UpdatePhoneStatusCommand : IRequest<Phone>
    {
        public long ConsumerId { get; set; }
        public long PhoneId { get; set; }
        public bool? Preferred { get; set; }
        public bool? Active { get; set; }
        public bool? Deleted { get; set; }
    }
}
