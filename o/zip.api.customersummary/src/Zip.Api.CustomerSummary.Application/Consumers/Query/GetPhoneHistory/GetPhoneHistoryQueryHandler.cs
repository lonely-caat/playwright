using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Consumers.Query.GetPhoneHistory
{
    public class GetPhoneHistoryQueryHandler : IRequestHandler<GetPhoneHistoryQuery, IEnumerable<Phone>>
    {
        private readonly IPhoneContext _phoneContext;

        public GetPhoneHistoryQueryHandler(IPhoneContext  phoneContext)
        {
            _phoneContext = phoneContext ?? throw new ArgumentNullException(nameof(phoneContext));
        }

        public async Task<IEnumerable<Phone>> Handle(GetPhoneHistoryQuery request, CancellationToken cancellationToken)
        {
            return await _phoneContext.GetConsumerPhoneHistoryAsync(request.ConsumerId);
        }
    }
}
