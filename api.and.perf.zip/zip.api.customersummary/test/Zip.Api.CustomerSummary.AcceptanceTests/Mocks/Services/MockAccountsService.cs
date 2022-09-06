using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using MediatR;
using Refit;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;
using Zip.Services.Accounts.Contract.Account;
using Zip.Services.Accounts.Contract.Account.Status;
using Zip.Services.Accounts.Contract.Invalidation;
using Zip.Services.Accounts.Contract.Order;
using Zip.Services.Accounts.Contract.Tango;
using OrdersResponse = Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Models.OrdersResponse;
using OrderDetailResponse = Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Models.OrderDetailResponse;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Mocks.Services
{
    public class MockAccountsService : IAccountsService
    {
        private readonly IFixture _fixture;

        public MockAccountsService()
        {
            _fixture = new Fixture();
        }

        public Task<string> Ping()
        {
            return Task.FromResult(string.Empty);
        }

        public Task<AccountResponse> GetAccount(string accountId)
        {
            return Task.FromResult(new AccountResponse
            {
                DueDayOfMonth = _fixture.Create<int>(),
                ArrearsDate = _fixture.Create<DateTime>(),
                LoanMgtAccount = new LoanMgtAccount
                {
                    DirectDebitNextDateDueAsAt = _fixture.Create<DateTime>().ToLongDateString(),
                    DirectDebitAmountDueAsAt = _fixture.Create<decimal>(),
                    DirectDebitFrequencyAsAt = "M"
                }
            });
        }

        public Task<AccountResponse> UpdateConfiguration(UpdateConfigurationRequest request)
        {
            if (!request.Configuration.CreditLimit.HasValue ||
                !request.Configuration.InterestRate.HasValue ||
                !request.Configuration.InstallmentAccount.HasValue)
            {
                return Task.FromResult(new AccountResponse());
            }

            var response = new AccountResponse
            {
                Id = request.AccountId.ToString(),
                Configuration =
                {
                    CreditLimit = request.Configuration.CreditLimit.Value,
                    InterestRate = request.Configuration.InterestRate.Value,
                    InstallmentAccount = request.Configuration.InstallmentAccount.Value
                }
            };

            return Task.FromResult(response);
        }

        public Task<CloseAccountResponse> CloseAccount(
            long accountId,
            [Header("X-Correlation-Id")] string correlationId)
        {
            return Task.FromResult(new CloseAccountResponse
            {
                Id = _fixture.Create<string>(),
                AvailableBalance = _fixture.Create<decimal>(),
                DueDayOfMonth = _fixture.Create<int>(),
                ArrearsDate = _fixture.Create<DateTime>(),
                CreatedDate = _fixture.Create<DateTime>(),
                Balance = _fixture.Create<decimal>(),
                Configuration = _fixture.Create<AccountResponseConfiguration>(),
                HasTransacted = _fixture.Create<bool>(),
                IsInArrears = _fixture.Create<bool>(),
                LoanMgtAccount = _fixture.Create<LoanMgtAccount>(),
                PendingBalance = _fixture.Create<decimal>(),
                Product = _fixture.Create<Zip.Services.Accounts.Contract.ProductClassification>(),
                State = _fixture.Create<AccountState>(),
                SubState = _fixture.Create<AccountSubState>()
            });
        }

        public Task<CloseAccountResponse> CloseAccount(
            long accountId,
            [Body] CloseAccountRequest request,
            [Header("X-Correlation-Id")] string correlationId)
        {
            return CloseAccount(accountId, correlationId);
        }

        public Task<LockAccountResponse> LockAccount(long accountId, LockAccountRequest request)
        {
            return Task.FromResult(new LockAccountResponse()
            {
                Id = "lockAccountId",
                ArrearsDate = DateTime.Today.AddDays(1),
                Balance = 1,
                State = AccountState.Locked,
                SubState = AccountSubState.Other
            });
        }

        public Task<InvalidationResponse> Invalidate(InvalidationRequest request)
        {
            return Task.FromResult(new InvalidationResponse());
        }

        public Task<decimal> GetPayoutQuote(long accountId)
        {
            return Task.FromResult(200m);
        }

        public Task<IEnumerable<LoanMgtTransaction>> GetTangoTransactions(
            long accountId,
            DateTime startDate,
            DateTime endDate,
            bool includeAuthorisedTransactions = false)
        {
            return Task.FromResult<IEnumerable<LoanMgtTransaction>>(new List<LoanMgtTransaction>
            {
                new LoanMgtTransaction
                {
                    DateEntered = DateTime.Now.AddDays(-1),
                    Narrative = "test 1",
                    TransactionAmount = 123m,
                },
                new LoanMgtTransaction
                {
                    DateEntered = DateTime.Now.AddDays(-2),
                    Narrative = "test 2",
                    TransactionAmount = 123m,
                },
                new LoanMgtTransaction
                {
                    DateEntered = DateTime.Now.AddDays(-3),
                    Narrative = "test 3",
                    TransactionAmount = 123m,
                },
                new LoanMgtTransaction
                {
                    DateEntered = DateTime.Now.AddDays(-4),
                    Narrative = "test 4",
                    TransactionAmount = 123m,
                }
            });
        }

        public Task<OrdersResponse> GetOrders(long accountId)
        {
            return Task.FromResult(_fixture.Create<OrdersResponse>());
        }

        public Task<OrderDetailResponse> GetOrderDetail(long accountId, long orderId)
        {
            return Task.FromResult(_fixture.Create<OrderDetailResponse>());
        }

        public async Task TransferOrderBalance(long accountId, long orderId, TransferOrderRequest request)
        {
            await Task.FromResult(Unit.Value);
        }
    }
}