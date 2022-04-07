using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Serilog;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo.Models;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.Tango;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Services.Accounts.Contract.Account;

namespace Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo
{
    public class GetAccountInfoQueryHandler : IRequestHandler<GetAccountInfoQuery, GetAccountInfoQueryResult>
    {
        private readonly IConsumerContext _consumerContext;

        private readonly ICustomerAttributeContext _customerAttributeContext;

        private readonly IAccountTypeContext _accountTypeContext;

        private readonly IAccountsService _accountsService;
        
        private readonly IMapper _mapper;

        public GetAccountInfoQueryHandler(
            IConsumerContext consumerContext,
            ICustomerAttributeContext customerAttributeContext,
            IAccountTypeContext accountTypeContext,
            IAccountsService accountsService,
            IMapper mapper)
        {
            _consumerContext = consumerContext ?? throw new ArgumentNullException(nameof(consumerContext));
            _customerAttributeContext = customerAttributeContext ?? throw new ArgumentNullException(nameof(customerAttributeContext));
            _accountTypeContext = accountTypeContext ?? throw new ArgumentNullException(nameof(accountTypeContext));
            _accountsService = accountsService;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<GetAccountInfoQueryResult> Handle(GetAccountInfoQuery request, CancellationToken cancellationToken)
        {
            var queryResult = new GetAccountInfoQueryResult();
            
            var accountInfo = await _consumerContext.GetAccountInfoAsync(request.ConsumerId);

            queryResult.AccountInfo = accountInfo ?? throw new AccountNotFoundException { ConsumerId = request.ConsumerId };
            queryResult.AccountInfo.Attributes = await _customerAttributeContext.GetConsumerAttributesAsync(queryResult.AccountInfo.ConsumerId);
            queryResult.AccountType = await _accountTypeContext.GetAsync(accountInfo.AccountTypeId);
            
            try
            {
                var accountResponse = await _accountsService.GetAccount(queryResult.AccountInfo.AccountHash);

                Log.Information($"{nameof(GetAccountInfoQueryHandler)} :: {nameof(Handle)} : LMS ACCOUNT RESPONSE", accountResponse);
                
                var lmsAccount = _mapper.Map<LmsAccountDto>(
                    accountResponse,
                    opt =>
                    {
                        opt.Items["CreditStatus"] = GetLmsAccountCreditStatus(accountResponse);
                    });

                queryResult.LmsAccount = lmsAccount;

                queryResult.ArrearsDueDate = DateTime.Now.Date.AddDays (-queryResult.LmsAccount.DaysInArrears ?? 0 );

                queryResult.Configuration = accountResponse?.Configuration;
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message, queryResult.AccountInfo.AccountHash);
                
                queryResult.LmsAccount = null;
            }

            try
            {
                queryResult.PayoutQuote = await _accountsService.GetPayoutQuote(queryResult.AccountInfo.AccountId);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message, queryResult.AccountInfo.AccountHash);
                
                queryResult.PayoutQuote = null;
            }

            return queryResult;
        }

        private static string GetLmsAccountCreditStatus(AccountResponse accountResponse)
        {
            switch (accountResponse.State)
            {
                case AccountState.Active:
                    return accountResponse.HasTransacted == true ? LoanMgt.OperationalStatus : LoanMgt.NewAccountStatus;

                case AccountState.Closed:
                case AccountState.ChargedOff:
                    return accountResponse.LoanMgtAccount.GeneralPurpose1;
                
                default:
                    return MapFromLockedState(accountResponse);
            }
        }

        private static string MapFromLockedState(AccountResponse accountResponse)
        {
            if (accountResponse.State != AccountState.Locked)
            {
                return string.Empty;
            }

            switch (accountResponse.SubState)
            {
                case AccountSubState.Fraud:
                case AccountSubState.Other:
                    return LoanMgt.NoFurtherDrawDownStatus;
                
                case AccountSubState.Arrears:
                    return LoanMgt.OperationalStatus;
                
                default:
                    return accountResponse.LoanMgtAccount.GeneralPurpose1;
            }
        }
    }
}
