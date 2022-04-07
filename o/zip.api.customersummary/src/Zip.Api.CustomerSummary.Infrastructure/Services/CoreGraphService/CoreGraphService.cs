using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Request.GetUpcomingInstallments;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Request.PayOrder;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Response;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Response.PayOrder;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService
{
    public class CoreGraphService : ICoreGraphService
    {
        private const string GetUpcomingInstallmentsQuery =
            "query AccountV1($accountV1Input: AccountV1Input!, $upcomingInstallmentsInput: UpcomingInstallmentsInput!) { accountV1(input: $accountV1Input) { upcomingInstallments(input: $upcomingInstallmentsInput) { orderId, displayName, amount, date, status } } }";

        private const string PayOrderQuery =
            "mutation makeRepaymentForOrder($input: MakeRepaymentForOrderInput!) { makeRepaymentForOrder (input: $input) { code, message, success } }";

        private readonly ILogger<CoreGraphService> _logger;
        
        private readonly ICoreGraphServiceProxy _coreGraphServiceProxy;

        public CoreGraphService(
            ILogger<CoreGraphService> logger,
            ICoreGraphServiceProxy coreGraphServiceProxy)
        {
            _logger = logger;
            _coreGraphServiceProxy = coreGraphServiceProxy;
        }

        public async Task<CoreGraphServiceHealthCheckResponse> HealthCheckAsync()
        {
            return await _coreGraphServiceProxy.HealthCheckAsync();
        }

        public async Task<IEnumerable<Installment>> GetUpcomingInstallmentsAsync(long accountId, Guid customerId, DateTime toDate)
        {
            var logSuffix = $"for AccountId {accountId}, CustomerId {customerId}, ToDate {toDate}";
            
            try
            {
                LogInformation(nameof(GetUpcomingInstallmentsAsync), $"Start processing {nameof(GetUpcomingInstallmentsAsync)} {logSuffix}");

                var request =
                    new GetUpcomingInstallmentsRequest(GetUpcomingInstallmentsQuery, accountId, customerId, toDate);

                var response = await _coreGraphServiceProxy.GetUpcomingInstallmentsAsync(request);

                LogInformation(nameof(GetUpcomingInstallmentsAsync), $"Finished processing {nameof(GetUpcomingInstallmentsAsync)} with response {response.ToJsonString()} {logSuffix}");

                return response?.Data?.AccountV1?.UpcomingInstallments;
            }
            catch (Refit.ApiException rex)
            {
                if (rex.StatusCode == HttpStatusCode.NotFound)
                {
                    LogInformation(
                        nameof(GetUpcomingInstallmentsAsync),
                        $"Finished {nameof(GetUpcomingInstallmentsAsync)} {logSuffix}. Upcoming installments not found.");

                    return null;
                }

                LogError(rex,
                         nameof(GetUpcomingInstallmentsAsync),
                         $"Failed to {nameof(GetUpcomingInstallmentsAsync)} {logSuffix}." +
                         $"Refit API exception with message: {rex.Message}");

                throw new CoreGraphException(rex.Message, rex);
            }
            catch (Exception ex) {
                LogError(ex,
                         nameof(GetUpcomingInstallmentsAsync),
                         $"Failed to process {nameof(GetUpcomingInstallmentsAsync)} with message {ex.Message} {logSuffix}");

                throw;
            }
        }

        public async Task<PayOrderInnerResponse> PayOrderAsync(PayOrderInput input)
        {
            var logSuffix = $"for Request: {input.ToJsonString()}";

            try
            {
                var ipV4Address = (await Dns.GetHostEntryAsync(Dns.GetHostName()))
                                 .AddressList
                                 .FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork)
                                ?.ToString();

                LogInformation(nameof(PayOrderAsync), $"Start processing {nameof(PayOrderAsync)} {logSuffix}");

                var request = new PayOrderRequest(PayOrderQuery,
                                                  input.AccountId,
                                                  input.CustomerId,
                                                  input.OrderId,
                                                  input.Amount);

                var response = await _coreGraphServiceProxy.PayOrderAsync(ipV4Address, request);

                LogInformation(nameof(PayOrderAsync), $"Finished processing {nameof(PayOrderAsync)} with response {response?.ToJsonString()} {logSuffix}");

                return response?.Data?.InnerResponse;
            }
            catch(Refit.ApiException rex)
            {
                LogError(rex,
                         nameof(PayOrderAsync),
                         $"Failed to {nameof(PayOrderAsync)} {logSuffix}." +
                         $"Refit API exception with message: {rex.Message}");

                throw new CoreGraphException(rex.Message, rex);
            }
            catch (Exception ex)
            {
                LogError(ex,
                         nameof(PayOrderAsync),
                         $"Failed to process {nameof(PayOrderAsync)} with message {ex.Message} {logSuffix}");

                throw;
            }
        }

        private void LogInformation(string methodName, string message)
        {
            _logger.LogInformation(
                $"{SerilogProperty.ClassName} :: {SerilogProperty.MethodName} :: {SerilogProperty.Message}",
                nameof(CoreService),
                methodName,
                message
            );
        }

        private void LogError(Exception ex, string methodName, string message)
        {
            _logger.LogError(ex,
                             $"{SerilogProperty.ClassName} :: {SerilogProperty.MethodName} :: {SerilogProperty.Message}",
                             nameof(CoreService),
                             methodName,
                             message
            );
        }
    }
}
