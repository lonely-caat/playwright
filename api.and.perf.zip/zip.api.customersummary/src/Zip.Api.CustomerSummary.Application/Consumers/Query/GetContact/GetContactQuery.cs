using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;

namespace Zip.Api.CustomerSummary.Application.Consumers.Query.GetContact
{
    public class GetContactQuery : IRequest<ContactDto>
    {
        public long ConsumerId { get; set; }

        public GetContactQuery(long consumerId)
        {
            ConsumerId = consumerId;
        }

        public GetContactQuery()
        {

        }
    }
}
