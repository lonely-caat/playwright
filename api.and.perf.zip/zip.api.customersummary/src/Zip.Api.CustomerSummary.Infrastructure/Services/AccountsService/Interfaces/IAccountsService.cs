using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Services.Accounts.Contract.Account;
using Zip.Services.Accounts.Contract.Account.Status;
using Zip.Services.Accounts.Contract.Invalidation;
using Zip.Services.Accounts.Contract.Order;
using Zip.Services.Accounts.Contract.Tango;
using OrderDetailResponse = Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Models.OrderDetailResponse;
using OrdersResponse = Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Models.OrdersResponse;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces
{
    public interface IAccountsService
    {
        Task<string> Ping();

        Task<AccountResponse> GetAccount(string accountId);

        Task<AccountResponse> UpdateConfiguration(UpdateConfigurationRequest request);

        Task<CloseAccountResponse> CloseAccount(long accountId, string correlationId);
        
        Task<CloseAccountResponse> CloseAccount(long accountId, CloseAccountRequest request, string correlationId);

        Task<LockAccountResponse> LockAccount(long accountId, LockAccountRequest request);

        Task<InvalidationResponse> Invalidate(InvalidationRequest request);

        Task<decimal> GetPayoutQuote(long accountId);
        
        Task<IEnumerable<LoanMgtTransaction>> GetTangoTransactions(long accountId, DateTime startDate, DateTime endDate, bool includeAuthorisedTransactions = false);

        Task<OrdersResponse> GetOrders(long accountId);

        Task<OrderDetailResponse> GetOrderDetail(long accountId, long orderId);

        Task TransferOrderBalance(long accountId, long orderId, TransferOrderRequest request);
    }
}
