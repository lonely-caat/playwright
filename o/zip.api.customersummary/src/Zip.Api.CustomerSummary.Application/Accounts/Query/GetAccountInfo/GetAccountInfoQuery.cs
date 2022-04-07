using MediatR;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo.Models;

namespace Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo
{
    public class GetAccountInfoQuery : IRequest<GetAccountInfoQueryResult>
    {
        public long ConsumerId { get; private set; }

        public GetAccountInfoQuery(long consumerId)
        {
            ConsumerId = consumerId;
        }

        public GetAccountInfoQuery()
        {

        }
    }
}
