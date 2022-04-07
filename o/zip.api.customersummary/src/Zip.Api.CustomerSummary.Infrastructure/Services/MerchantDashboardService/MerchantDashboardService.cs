using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Refit;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.MerchantDashboardService.Interface;
using Zip.Api.CustomerSummary.Infrastructure.Services.MerchantDashboardService.Models.Requests;
using Zip.Api.CustomerSummary.Infrastructure.Services.MerchantDashboardService.Models.Responses;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.MerchantDashboardService
{
    public class MerchantDashboardService : IMerchantDashboardService
    {
        private readonly ILogger<MerchantDashboardService> _logger;
        private readonly IMerchantDashboardApiProxy _proxy;

        public MerchantDashboardService(
            ILogger<MerchantDashboardService> logger,
            IMerchantDashboardApiProxy proxy)
        {
            _logger = logger;
            _proxy = proxy;
        }

        public async Task<OrderSummaryResponse> GetOrderSummaryAsync(OrderSummaryRequest request, CancellationToken cancellationToken)
        {
            var logSuffix = $"Request: {request.ToJsonString()}";

            try
            {
                LogInformation($"Start sending {nameof(OrderSummaryRequest)}. {logSuffix}", nameof(GetOrderSummaryAsync));

                using var httpResponseMessage = await _proxy.SendGetOrderSummaryRequestAsync(request, cancellationToken);

                if (!httpResponseMessage.EnsureSuccessStatusCode(out var content))
                {
                    throw new MerchantDashboardApiException(
                        $"Failed to get {nameof(OrderSummaryResponse)} for {logSuffix}, StatusCode:{httpResponseMessage.StatusCode}, Content: {content}");
                }

                var merchantDemographicReportResponse = JsonConvert.DeserializeObject<OrderSummaryResponse>(content);

                LogInformation($"Successfully retrieved {nameof(OrderSummaryResponse)}. {logSuffix}", nameof(GetOrderSummaryAsync));

                return merchantDemographicReportResponse;
            }
            catch (ApiException rex)
            {
                LogError($"Failed to send {nameof(OrderSummaryRequest)}. {logSuffix}. Refit API exception with message: {rex.Message}",
                         nameof(GetOrderSummaryAsync),
                         rex);

                throw;
            }
            catch (Exception ex)
            {
                LogError($"Failed to process {nameof(OrderSummaryRequest)}. {logSuffix}. Exception Message: {ex.Message}",
                         nameof(GetOrderSummaryAsync),
                         ex);

                throw;
            }
        }

        private void LogInformation(string message, string methodName)
        {
            _logger.LogInformation($"{SerilogProperty.ClassName} :: {SerilogProperty.MethodName} :: {SerilogProperty.Message}",
                                   nameof(MfaService),
                                   methodName,
                                   message);
        }

        private void LogError(string message, string methodName, Exception ex)
        {
            _logger.LogError(ex,
                             $"{SerilogProperty.ClassName} :: {SerilogProperty.MethodName} :: {SerilogProperty.Message}",
                             nameof(MfaService),
                             methodName,
                             message);
        }
    }
}
