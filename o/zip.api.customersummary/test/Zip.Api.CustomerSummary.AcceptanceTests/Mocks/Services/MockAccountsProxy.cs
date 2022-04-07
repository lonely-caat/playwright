using AutoFixture;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Zip.Services.Accounts.Contract.Account;
using Zip.Services.Accounts.Contract.Account.Status;
using Zip.Services.Accounts.Contract.Installment;
using Zip.Services.Accounts.Contract.Invalidation;
using Zip.Services.Accounts.Contract.Order;
using Zip.Services.Accounts.Contract.OrderAccount;
using Zip.Services.Accounts.Contract.Tango;
using Zip.Services.Accounts.Contract.Transaction;
using Zip.Services.Accounts.ServiceProxy;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Mocks.Services
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
            throw new NotImplementedException();
        }

        public Task<CanUnlockResponse> CanUnlock(long accountId)
        {
            throw new NotImplementedException();
        }

        public Task<CloseAccountResponse> CloseAccount(long accountId, [Header("X-Correlation-Id")] string correlationId)
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

        public Task<CloseAccountResponse> CloseAccount(long accountId, [Body] CloseAccountRequest request, [Header("X-Correlation-Id")] string correlationId)
        {
            return this.CloseAccount(accountId, correlationId);
        }

        public Task<CreateAccountResponse> CreateAccount([Body] CreateAccountRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<CreateTransactionResponse> CreateTransaction(string accountId, [Body] CreateTransactionRequest request)
        {
            throw new NotImplementedException();
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

        public Task<OrderAccountPaymentResponse> GetOrderAccountPayments(long accountId, [Query(Format = "s")] DateTime from, [Query(Format = "s")] DateTime to)
        {
            throw new NotImplementedException();
        }

        public Task<OrderAccountResponse> GetOrderAccounts(long accountId, bool onlyOutstanding = true)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDetailResponse> GetOrderDetail(long accountId, long orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CreateAdditionalBatchPaymentResponse>> CreateAdditionalBatchPayment(long accountId, AdditionalBatchPayment additionalBatchPayment)
        {
            throw new NotImplementedException();
        }

        public async Task RepairSchedule(long accountId)
        {
            throw new NotImplementedException();
        }

        public Task<OrdersResponse> GetOrders(long accountId)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> GetPayoutQuote(long accountId)
        {
            return Task.FromResult(200m);
        }

        public Task<IEnumerable<LoanMgtTransaction>> GetTangoTransactions(long accountId, [Query(Format = "s")] DateTime startDate, [Query(Format = "s")] DateTime endDate, bool includeAuthorisedTransactions = false)
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

        public async Task TransferOrderBalance(long accountId, long orderId, TransferOrderRequest request)
        {
            await Task.FromResult(Unit.Value);
        }

        public Task<GetTransactionResponse> GetTransaction(long accountId, long transactionHistoryId)
        {
            throw new NotImplementedException();
        }

        public Task<GetTransactionsResponse> GetTransactions(long accountId, [Query(Format = "s")] DateTime fromDate, [Query(Format = "s")] DateTime untilDate, int? pageSize = null, int? pageNumber = null)
        {
            throw new NotImplementedException();
        }

        public Task<GetTransactionsResponse> GetTransactionsByOrderId(long accountId, long orderId)
        {
            throw new NotImplementedException();
        }

        public Task<InvalidationResponse> Invalidate(InvalidationRequest request)
        {
            return Task.FromResult(new InvalidationResponse());
        }

        public Task<CustomerAccountsInvalidationResponse> InvalidateCustomerAccounts(CustomerAccountsInvalidationRequest request)
        {
            return Task.FromResult(new CustomerAccountsInvalidationResponse());
        }

        public Task<LockAccountResponse> LockAccount(long accountId, [Body] LockAccountRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<string> Ping()
        {
            throw new NotImplementedException();
        }

        public Task<ReopenAccountResponse> ReopenAccount(long accountId, [Body] ReopenAccountRequest request, [Header("X-Correlation-Id")] string correlationId)
        {
            throw new NotImplementedException();
        }

        public Task<List<AccountResponse>> SearchAccounts([Query] string customerId, [Query] bool onlyTransactableAccounts = false)
        {
            throw new NotImplementedException();
        }

        public Task<UnlockAccountResponse> UnlockAccount(long accountId)
        {
            throw new NotImplementedException();
        }

        public Task<UnWriteOffAccountResponse> UnWriteOffAccount(long accountId, [Body] UnWriteOffAccountRequest request, [Header("Idempotency-Key")] string idempotencyKey, [Header("X-Correlation-Id")] string correlationId)
        {
            throw new NotImplementedException();
        }

        public Task<AccountResponse> UpdateConfiguration(long accountId, [Body] UpdateConfigurationRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateCustomerResponse> UpdateCustomer(long accountId, [Body] UpdateCustomerRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<VoidTransactionResponse> VoidTransaction(string accountId, string transactionId, [Body] VoidTransactionRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<WriteOffAccountResponse> WriteOffAccount(long accountId, [Body] WriteOffAccountRequest request, [Header("Idempotency-Key")] string idempotencyKey, [Header("X-Correlation-Id")] string correlationId)
        {
            throw new NotImplementedException();
        }
    }
}
