using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;
using Zip.Services.Accounts.Contract.Account;
using Zip.Services.Accounts.Contract.Account.Status;
using Zip.Services.Accounts.Contract.Invalidation;
using Zip.Services.Accounts.Contract.Order;
using Zip.Services.Accounts.Contract.Tango;
using Zip.Services.Accounts.ServiceProxy;
using OrderDetailResponse = Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Models.OrderDetailResponse;
using OrdersResponse = Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Models.OrdersResponse;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService
{
    public class AccountsService : IAccountsService
    {
        private readonly IAccountsProxy _accountsProxy;

        private readonly ILogger<AccountsService> _logger;

        private readonly IMapper _mapper;

        public AccountsService(
            IAccountsProxy accountsProxy,
            ILogger<AccountsService> logger,
            IMapper mapper)
        {
            _accountsProxy = accountsProxy;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<string> Ping()
        {
            return await _accountsProxy.Ping();
        }

        public async Task<AccountResponse> GetAccount(string accountId)
        {
            var logSuffix = $"for AccountId {accountId}";
            try
            {
                LogInformation(nameof(GetAccount), $"Start {nameof(GetAccount)} {logSuffix}");

                var response = await _accountsProxy.GetAccount(accountId);

                LogInformation(nameof(GetAccount), $"Finished {nameof(GetAccount)} {logSuffix}");

                return response;
            }
            catch (Exception ex)
            {
                LogError(nameof(GetAccount), ex);
                throw new AccountsApiException($"Failed to {nameof(GetAccount)} with AccountId {accountId}", ex);
            }
        }

        public async Task<AccountResponse> UpdateConfiguration(UpdateConfigurationRequest request)
        {
            var logSuffix = $"for Request {request.ToJsonString()}";
            try
            {
                LogInformation(nameof(UpdateConfiguration), $"Start {nameof(UpdateConfiguration)} {logSuffix}");

                var response = await _accountsProxy.UpdateConfiguration(request.AccountId, request);

                LogInformation(nameof(UpdateConfiguration), $"Finished {nameof(UpdateConfiguration)} {logSuffix}");

                return response;
            }
            catch (Exception ex)
            {
                LogError(nameof(UpdateConfiguration), ex);
                throw new AccountsApiException($"Failed to {nameof(UpdateConfiguration)} {logSuffix}",
                                               ex);
            }
        }

        public async Task<CloseAccountResponse> CloseAccount(long accountId, string correlationId)
        {
            var logSuffix = $"for AccountId: {accountId}, CorrelationId: {correlationId}";
            try
            {
                LogInformation(nameof(CloseAccount), $"Start {nameof(CloseAccount)} {logSuffix}");

                var response = await _accountsProxy.CloseAccount(accountId, correlationId);

                LogInformation(nameof(CloseAccount), $"Finished {nameof(CloseAccount)} {logSuffix}");

                return response;
            }
            catch (Exception ex)
            {
                LogError(nameof(CloseAccount), ex);
                throw new AccountsApiException(
                    $"Failed to {nameof(CloseAccount)} {logSuffix}",
                    ex);
            }
        }

        public async Task<CloseAccountResponse> CloseAccount(
            long accountId,
            CloseAccountRequest request,
            string correlationId)
        {
            var logSuffix =
                $"for AccountId {accountId}, Request {request.ToJsonString()}, CorrelationId {correlationId}";
            try
            {
                LogInformation(nameof(CloseAccount), $"Start {nameof(CloseAccount)} {logSuffix}");

                var response = await _accountsProxy.CloseAccount(accountId, request, correlationId);

                LogInformation(nameof(CloseAccount), $"Finished {nameof(CloseAccount)} {logSuffix}");

                return response;
            }
            catch (Exception ex)
            {
                LogError(nameof(CloseAccount), ex);
                throw new AccountsApiException(
                    $"Failed to {nameof(CloseAccount)} {logSuffix}",
                    ex);
            }
        }

        public async Task<LockAccountResponse> LockAccount(long accountId, LockAccountRequest request)
        {
            var logSuffix = $"for AccountId {accountId}, Request {request.ToJsonString()}";
            try
            {
                LogInformation(nameof(LockAccount), $"Start {nameof(LockAccount)} {logSuffix}");

                var response = await _accountsProxy.LockAccount(accountId, request);

                LogInformation(nameof(LockAccount), $"Finished {nameof(LockAccount)} {logSuffix}");

                return response;
            }
            catch (Exception ex)
            {
                LogError(nameof(LockAccount), ex);
                throw new AccountsApiException(
                    $"Failed to {nameof(LockAccount)} {logSuffix}",
                    ex);
            }
        }

        public async Task<InvalidationResponse> Invalidate(InvalidationRequest request)
        {
            var logSuffix = $"for Request {request.ToJsonString()}";
            try
            {
                LogInformation(nameof(Invalidate), $"Start {nameof(Invalidate)} {logSuffix}");

                var response = await _accountsProxy.Invalidate(request);

                LogInformation(nameof(Invalidate), $"Finished {nameof(Invalidate)} {logSuffix}");

                return response;
            }
            catch (Exception ex)
            {
                LogError(nameof(Invalidate), ex);
                throw new AccountsApiException($"Failed to {nameof(Invalidate)} {logSuffix}", ex);
            }
        }

        public async Task<decimal> GetPayoutQuote(long accountId)
        {
            var logSuffix = $"for AccountId {accountId}";
            try
            {
                LogInformation(nameof(GetPayoutQuote), $"Start {nameof(GetPayoutQuote)} {logSuffix}");

                var response = await _accountsProxy.GetPayoutQuote(accountId);

                LogInformation(nameof(GetPayoutQuote), $"Finished {nameof(GetPayoutQuote)} {logSuffix}");

                return response;
            }
            catch (Exception ex)
            {
                LogError(nameof(GetPayoutQuote), ex);
                throw new AccountsApiException($"Failed to {nameof(GetPayoutQuote)} {logSuffix}", ex);
            }
        }

        public async Task<IEnumerable<LoanMgtTransaction>> GetTangoTransactions(
            long accountId,
            DateTime startDate,
            DateTime endDate,
            bool includeAuthorisedTransactions = false)
        {
            var logSuffix =
                $"for AccountId {accountId}, StartDate {startDate}, EndDate {endDate}, IncludeAuthorisedTransaction {includeAuthorisedTransactions}";
            try
            {
                LogInformation(nameof(GetTangoTransactions), $"Start {nameof(GetTangoTransactions)} {logSuffix}");

                var response = await _accountsProxy.GetTangoTransactions(accountId,
                                                                         startDate,
                                                                         endDate,
                                                                         includeAuthorisedTransactions);

                LogInformation(nameof(GetTangoTransactions), $"Finished {nameof(GetTangoTransactions)} {logSuffix}");

                return response;
            }
            catch (Exception ex)
            {
                LogError(nameof(GetTangoTransactions), ex);
                throw new AccountsApiException(
                    $"Failed to {nameof(GetTangoTransactions)} {logSuffix}",
                    ex);
            }
        }

        public async Task<OrdersResponse> GetOrders(long accountId)
        {
            var logSuffix = $"for AccountId {accountId}";
            try
            {
                LogInformation(nameof(GetOrders), $"Start {nameof(GetOrders)} {logSuffix}");

                var response = await _accountsProxy.GetOrders(accountId);

                LogInformation(nameof(GetOrders),
                               $"Mapping response from {nameof(IAccountsProxy)}:{nameof(GetOrders)} {logSuffix}");

                var result = _mapper.Map<OrdersResponse>(response);

                LogInformation(nameof(GetOrders), $"Finished {nameof(GetOrders)} {logSuffix}");

                return result;
            }
            catch (Exception ex)
            {
                LogError(nameof(GetOrders), ex);
                throw new AccountsApiException($"Failed to {nameof(GetOrders)} {logSuffix}", ex);
            }
        }

        public async Task<OrderDetailResponse> GetOrderDetail(long accountId, long orderId)
        {
            var logSuffix = $"for OrderId {orderId} for AccountId {accountId}";
            try
            {
                LogInformation(nameof(GetOrderDetail), $"Start {nameof(GetOrderDetail)} {logSuffix}");

                var response = await _accountsProxy.GetOrderDetail(accountId, orderId);

                LogInformation(nameof(GetOrders),
                               $"Mapping response from {nameof(IAccountsProxy)}:{nameof(GetOrderDetail)} {logSuffix}");

                var result = _mapper.Map<OrderDetailResponse>(response);

                LogInformation(nameof(GetOrderDetail), $"Finished {nameof(GetOrderDetail)} {logSuffix}");

                return result;
            }
            catch (Exception ex)
            {
                LogError(nameof(GetOrderDetail), ex);
                throw new AccountsApiException($"Failed to {nameof(GetOrderDetail)} {logSuffix}", ex);
            }
        }

        public async Task TransferOrderBalance(long accountId, long orderId, TransferOrderRequest request)
        {
            var logSuffix = $"for OrderId {orderId} for AccountId {accountId} and Request {request.ToJsonString()}";
            try
            {
                LogInformation(nameof(TransferOrderBalance), $"Start {nameof(TransferOrderBalance)} {logSuffix}");

                await _accountsProxy.TransferOrderBalance(accountId, orderId, request);

                LogInformation(nameof(TransferOrderBalance), $"Finished {nameof(TransferOrderBalance)} {logSuffix}");
            }
            catch (Exception ex)
            {
                LogError(nameof(TransferOrderBalance), ex);
                throw new AccountsApiException($"Failed to {nameof(TransferOrderBalance)} {logSuffix}", ex);
            }
        }

        private void LogInformation(string methodName, string message)
        {
            _logger.LogInformation(
                $"{SerilogProperty.ClassName} :: {SerilogProperty.MethodName} : {SerilogProperty.Message}",
                nameof(AccountsService),
                methodName,
                message);
        }

        private void LogError(string methodName, Exception ex)
        {
            _logger.LogError(ex,
                             $"{SerilogProperty.ClassName} :: {SerilogProperty.MethodName} : {SerilogProperty.Message}",
                             nameof(AccountsService),
                             methodName,
                             ex.Message);
        }
    }
}