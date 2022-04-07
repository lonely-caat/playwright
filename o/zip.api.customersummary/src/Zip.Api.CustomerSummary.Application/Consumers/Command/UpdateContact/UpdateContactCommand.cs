using MediatR;

namespace Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateContact
{
    public class UpdateContactCommand : IRequest
    {
        public long ConsumerId { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
    }
}
