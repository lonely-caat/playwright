using MediatR;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetCloseAccountCreditProfile.Models;

namespace Zip.Api.CustomerSummary.Application.Accounts.Query.GetCloseAccountCreditProfile
{
    public class GetCloseAccountCreditProfileQuery : IRequest<GetCloseAccountCreditProfileQueryResult>
    {
        public long ConsumerId { get; set; }

        public GetCloseAccountCreditProfileQuery(long consumerId)
        {
            ConsumerId = consumerId;
        }

        public GetCloseAccountCreditProfileQuery()
        {

        }
    }
}
