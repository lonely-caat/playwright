using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetRepaymentSchedule.Models;
using Zip.Api.CustomerSummary.Domain.Entities.Payments;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Services.Accounts.Contract.Account;

namespace Zip.Api.CustomerSummary.Application.Payments.Query.GetRepaymentSchedule
{
    public class GetRepaymentScheduleQueryHandler : IRequestHandler<GetRepaymentScheduleQuery, GetRepaymentScheduleQueryResult>
    {
        private readonly IAccountContext _accountContext;
        
        private readonly IMediator _mediator;
        
        private readonly IAccountsService _accountsService;

        public GetRepaymentScheduleQueryHandler(
            IAccountContext accountContext,
            IMediator mediator,
            IAccountsService accountsService)
        {
            _accountContext = accountContext ?? throw new ArgumentNullException(nameof(accountContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _accountsService = accountsService;
        }

        public async Task<GetRepaymentScheduleQueryResult> Handle(GetRepaymentScheduleQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountContext.GetAsync(request.AccountId);

            if (account == null)
            {
                throw new AccountNotFoundException(default, request.AccountId);
            }

            var result = new GetRepaymentScheduleQueryResult
            {
                Schedule = await _accountContext.GetRepaymentScheduleAsync(request.AccountId)
            };

            var accountInfo = await _mediator.Send(new GetAccountInfoQuery(account.ConsumerId), cancellationToken);

            if (accountInfo?.LmsAccount != null)
            {
                result.Schedule.ContractualDate = accountInfo.LmsAccount.ContractualDate;
                result.Schedule.ContractualAmount= accountInfo.LmsAccount.ContractualAmount;
            }

            var accountResponse = await _accountsService.GetAccount(request.AccountId.ToString());

            if (accountResponse?.LoanMgtAccount != null)
            {
                result.NextRepayment = new NextPayment
                {
                    Amount = accountResponse.LoanMgtAccount.DirectDebitAmountDueAsAt,
                    Frequency = GetFrequency(accountResponse),
                    StartDate = GetDirectDebitDueDate(accountResponse)
                };
            }

            return result;
        }

        public DateTime? GetDirectDebitDueDate(AccountResponse accountResponse)
        {
            if (string.IsNullOrEmpty(accountResponse?.LoanMgtAccount?.DirectDebitNextDateDueAsAt))
            {
                return null;
            }

            return DateTime.Parse(accountResponse.LoanMgtAccount?.DirectDebitNextDateDueAsAt);
        }

        public Frequency GetFrequency(AccountResponse accountResponse)
        {
            if (string.IsNullOrEmpty(accountResponse.LoanMgtAccount.DirectDebitNextDateDueAsAt))
            {
                return Frequency.Monthly;
            }

            var frequency = Frequency.Monthly;
            switch (accountResponse.LoanMgtAccount.DirectDebitFrequencyAsAt)
            {
                case "W":
                    frequency = Frequency.Weekly;
                    break;
                case "F":
                    frequency = Frequency.Fortnightly;
                    break;
                case "M":
                    frequency = Frequency.Monthly;
                    break;
            }

            return frequency;
        }
    }
}
