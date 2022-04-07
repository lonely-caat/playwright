using System.Collections.Generic;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;

namespace Zip.Api.CustomerSummary.Application.Consumers.Query.GetPhoneHistory
{
    public class GetPhoneHistoryQuery : IRequest<IEnumerable<Phone>>
    {
        public long ConsumerId { get; set; }

        public GetPhoneHistoryQuery()
        {
        }

        public GetPhoneHistoryQuery(long consumerId)
        {
            ConsumerId = consumerId;
        }
    }
}
