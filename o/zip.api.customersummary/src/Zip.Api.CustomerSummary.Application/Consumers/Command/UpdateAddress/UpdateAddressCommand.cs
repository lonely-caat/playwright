using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;

namespace Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateAddress
{
    public class UpdateAddressCommand : IRequest
    {
        public long ConsumerId { get; set; }
        public Address Address { get; set; }

        public UpdateAddressCommand(long consumerId, Address address)
        {
            ConsumerId = consumerId;
            Address = address;
        }

        public UpdateAddressCommand()
        {

        }
    }
}
