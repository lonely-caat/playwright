using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using MediatR;
using Refit;
using Zip.Services.Accounts.Contract.Account;
using Zip.Services.Accounts.Contract.Account.Status;
using Zip.Services.Accounts.Contract.Installment;
using Zip.Services.Accounts.Contract.Invalidation;
using Zip.Services.Accounts.Contract.Order;
using Zip.Services.Accounts.Contract.OrderAccount;
using Zip.Services.Accounts.Contract.Tango;
using Zip.Services.Accounts.Contract.Transaction;
using Zip.Services.Accounts.ServiceProxy;

namespace Zip.Api.CustomerSummary.Api.Test.Mocks.Services
{
    public class MockAccountsProxy : IAccountsProxy
    {
        private readonly IFixture _fixture;

        public MockAccountsProxy()
        {
            _fixture = new Fixture();
        }

        public Task<CanAuthoriseResponse> CanAuthorise(string accountId, decimal amount)
        {
            return Task.FromResult(new CanAuthoriseResponse());
        }

        public Task<CanUnlockResponse> CanUnlock(long accountId)
        {
            return Task.FromResult(new CanUnlockResponse());
        }

        public Task<CloseAccountResponse> CloseAccount(long accountId, [Sidebar("X-Correlation-Id")] string correlationId)
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

        public Task<CloseAccountResponse> CloseAccount(long accountId, [Body] CloseAccountRequest request, [Sidebar("X-Correlation-Id")] string correlationId)
        {
            return this.CloseAccount(accountId, correlationId);
        }

        public Task<CreateAccountResponse> CreateAccount([Body] CreateAccountRequest request)
        {
            return Task.FromResult(new CreateAccountResponse());
        }

        public Task<CreateTransactionResponse> CreateTransaction(string accountId, [Body] CreateTransactionRequest request)
        {
            return Task.FromResult(new CreateTransactionResponse());
        }

        public Task<AccountResponse> GetAccount(string accountId)
        {
            var result = accountId == int.MaxValue.ToString() ? null : new AccountResponse
                {
                    DueDayOfMonth = _fixture.Create<int>(),
                    ArrearsDate = _fixture.Create<DateTime>(),
                    LoanMgtAccount = new LoanMgtAccount
                    {
                        DirectDebitNextDateDueAsAt = _fixture.Create<DateTime>().ToLongDateString(),
                        DirectDebitAmountDueAsAt = _fixture.Create<decimal>(),
                        DirectDebitFrequencyAsAt = "M"
                    }
                };

            return Task.FromResult(result);
        }

        public Task<decimal> GetPayoutQuote(long accountId)
        {
            return Task.FromResult(200m);
        }

        public Task<IEnumerable<LoanMgtTransaction>> GetTangoTransactions(long accountId, [Query(Format = "s")] DateTime startDate, [Query(Format = "s")] DateTime endDate, bool includeAuthorisedTransactions = false)
        {
            if(accountId == int.MaxValue)
                throw new Exception("test exception");

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

        public async Task TransferOrderBalance(long accountId, long orderId, TransferOrderRequest request)
        {
            await Task.FromResult(Unit.Value);
        }

        public Task<GetTransactionResponse> GetTransaction(long accountId, long transactionHistoryId)
        {
            return Task.FromResult(new GetTransactionResponse());
        }

        public Task<GetTransactionsResponse> GetTransactions(long accountId, [Query(Format = "s")] DateTime fromDate, [Query(Format = "s")] DateTime untilDate, int? pageSize = null, int? pageNumber = null)
        {
            return Task.FromResult(new GetTransactionsResponse());
        }

        public Task<InvalidationResponse> Invalidate(InvalidationRequest request)
        {
            return Task.FromResult(new InvalidationResponse());
        }

        public Task<CustomerAccountsInvalidationResponse> InvalidateCustomerAccounts(CustomerAccountsInvalidationRequest request)
        {
            return Task.FromResult(new CustomerAccountsInvalidationResponse());
        }

        public Task<GetTransactionsResponse> GetTransactionsByOrderId(long accountId, long orderId)
        {
            return Task.FromResult(new GetTransactionsResponse());
        }

        public Task<LockAccountResponse> LockAccount(long accountId, [Body] LockAccountRequest request)
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

        public Task<string> Ping()
        {
            return Task.FromResult(string.Empty);
        }

        public Task<ReopenAccountResponse> ReopenAccount(long accountId, [Body] ReopenAccountRequest request, [Sidebar("X-Correlation-Id")] string correlationId)
        {
            return Task.FromResult(new ReopenAccountResponse());
        }

        public Task<List<AccountResponse>> SearchAccounts([Query] string customerId, [Query] bool onlyTransactableAccounts = false)
        {
            return Task.FromResult(new List<AccountResponse>());
        }

        public Task<UnlockAccountResponse> UnlockAccount(long accountId)
        {
            return Task.FromResult(new UnlockAccountResponse());
        }

        public Task<UnWriteOffAccountResponse> UnWriteOffAccount(long accountId, [Body] UnWriteOffAccountRequest request, [Sidebar("Idempotency-Key")] string idempotencyKey, [Sidebar("X-Correlation-Id")] string correlationId)
        {
            return Task.FromResult(new UnWriteOffAccountResponse());
        }

        public Task<AccountResponse> UpdateConfiguration(long accountId, [Body] UpdateConfigurationRequest request)
        {
            return Task.FromResult(new AccountResponse());
        }

        public Task<OrderAccountPaymentResponse> GetOrderAccountPayments(long accountId, DateTime @from, DateTime to)
        {
            return Task.FromResult(new OrderAccountPaymentResponse());
        }

        public Task<OrderAccountResponse> GetOrderAccounts(long accountId, bool onlyOutstanding = true)
        {
            return Task.FromResult(new OrderAccountResponse());
        }

        public Task<OrdersResponse> GetOrders(long accountId)
        {
            return Task.FromResult(new OrdersResponse());
        }

        public Task<OrderDetailResponse> GetOrderDetail(long accountId, long orderId)
        {
            return Task.FromResult(new OrderDetailResponse());
        }

        public async Task<IEnumerable<CreateAdditionalBatchPaymentResponse>> CreateAdditionalBatchPayment(long accountId, AdditionalBatchPayment additionalBatchPayment)
        {
            throw new NotImplementedException();
        }

        public async Task RepairSchedule(long accountId)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateCustomerResponse> UpdateCustomer(long accountId, [Body] UpdateCustomerRequest request)
        {
            return Task.FromResult(new UpdateCustomerResponse());
        }

        public Task<VoidTransactionResponse> VoidTransaction(string accountId, string transactionId, [Body] VoidTransactionRequest request)
        {
            return Task.FromResult(new VoidTransactionResponse());
        }

        public Task<WriteOffAccountResponse> WriteOffAccount(long accountId, [Body] WriteOffAccountRequest request, [Sidebar("Idempotency-Key")] string idempotencyKey, [Sidebar("X-Correlation-Id")] string correlationId)
        {
            return Task.FromResult(new WriteOffAccountResponse());
        }
    }
}
