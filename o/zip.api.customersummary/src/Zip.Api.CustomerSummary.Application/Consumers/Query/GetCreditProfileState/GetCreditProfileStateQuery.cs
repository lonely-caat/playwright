using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;

namespace Zip.Api.CustomerSummary.Application.Consumers.Query.GetCreditProfileState
{
    public class GetCreditProfileStateQuery : IRequest<CreditProfileState>
    {
        public long ConsumerId { get; set; }

        public GetCreditProfileStateQuery(long consumerId)
        {
            ConsumerId = consumerId;
        }

        public GetCreditProfileStateQuery()
        {

        }
    }
}
