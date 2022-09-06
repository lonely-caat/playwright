using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountClosure
{
    public class GetAccountClosureQueryHandler : IRequestHandler<GetAccountClosureQuery, AccountClosureEnquireResult>
    {
        private readonly IAccountsService _accountsService;

        public GetAccountClosureQueryHandler(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        public async Task<AccountClosureEnquireResult> Handle(GetAccountClosureQuery request, CancellationToken cancellationToken)
        {

            var account = await _accountsService.GetAccount($"{request.AccountId}");

            if (account != null && (account.Balance > 0 || account.PendingBalance > 0))
            {
                return AccountClosureEnquireResult.PendingTransactions;
            }

            // avoid closing of the account for direct debit payments that are in clearing days.
            // This should avoid if the available balance is greater than the creditlimit since customer needs to pay the interest before closing
            if (account != null && account.AvailableBalance < account.Configuration?.CreditLimit)
            {
                return AccountClosureEnquireResult.TransactionsHaveFutureClearingDays;
            }

            return AccountClosureEnquireResult.GoodForClose;
        }
    }
}
