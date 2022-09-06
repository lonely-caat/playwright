using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Consumers.Query.GetContact
{
    public class GetContactQueryHandler : IRequestHandler<GetContactQuery, ContactDto>
    {
        private readonly IContactContext _contactContext;

        public GetContactQueryHandler(IContactContext contactContext)
        {
            _contactContext = contactContext ?? throw new ArgumentNullException(nameof(contactContext));
        }

        public async Task<ContactDto> Handle(GetContactQuery request, CancellationToken cancellationToken)
        {
            return await _contactContext.GetContactAsync(request.ConsumerId);
        }
    }
}
