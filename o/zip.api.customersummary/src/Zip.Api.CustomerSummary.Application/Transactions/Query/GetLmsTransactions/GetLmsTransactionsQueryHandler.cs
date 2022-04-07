using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Transactions;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Services.Accounts.Contract.Tango;

namespace Zip.Api.CustomerSummary.Application.Transactions.Query.GetLmsTransactions
{
    public class GetLmsTransactionsQueryHandler : IRequestHandler<GetLmsTransactionsQuery, IEnumerable<LmsTransaction>>
    {
        private readonly IAccountsService _accountsService;
        
        private readonly ITransactionHistoryContext _transactionHistoryContext;

        public GetLmsTransactionsQueryHandler(
            IAccountsService accountsService,
            ITransactionHistoryContext transactionHistoryContext)
        {
            _accountsService = accountsService;
            _transactionHistoryContext = transactionHistoryContext ?? throw new ArgumentNullException(nameof(transactionHistoryContext));
        }

        public async Task<IEnumerable<LmsTransaction>> Handle(GetLmsTransactionsQuery request, CancellationToken cancellationToken)
        {
            var startDate = DateTime.Now.AddMonths(-2).AddDays(1);
            var endDate = DateTime.Now.AddDays(1);

            var loanMgtTransactions =
                (await _accountsService.GetTangoTransactions(request.AccountId, startDate, endDate, true))?.ToList();
            
            if (loanMgtTransactions == null || !loanMgtTransactions.Any())
            {
                return null;
            }
            
            var transactionHistories = await _transactionHistoryContext.FindByAccountIdAsync(request.AccountId, startDate, endDate);

            var lmsTransactions = loanMgtTransactions
                                 .Select(x => MapLmsTransaction(x, transactionHistories.FirstOrDefault(y => y.ThreadId == x.ThreadID)))
                                 .OrderByDescending(x => x.TransactionDate);

            return lmsTransactions;
        }

        private static LmsTransaction MapLmsTransaction(
            LoanMgtTransaction loanMgtTransaction,
            TransactionHistory transactionHistory)
        {
            var transactionDate = DateTime.MinValue;
            
            if (DateTime.TryParse(loanMgtTransaction.TransactionDate, out var result))
            {
                transactionDate = result;
            }
            
            return new LmsTransaction
            {
                TransactionDate = transactionDate,
                TransactionType = transactionHistory?.Type.ToString() ?? "N/A",
                Narrative = loanMgtTransaction.Narrative,
                Amount = loanMgtTransaction.TransactionAmount
            };
        }
    }
}
