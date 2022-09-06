using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;

namespace Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountClosure
{
    public class GetAccountClosureQuery : IRequest<AccountClosureEnquireResult>
    {
        public long AccountId { get; set; }

        public GetAccountClosureQuery(long accountId)
        {
            AccountId = accountId;
        }

        public GetAccountClosureQuery()
        {

        }
    }
}
