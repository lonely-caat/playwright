using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Refit;
using Zip.Api.CustomerSummary.Domain.Entities.Mfa;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.MfaService.Interfaces;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.MfaService
{
    public class MfaService : IMfaService
    {
        private readonly ILogger<MfaService> _logger;

        private readonly IMfaProxy _mfaProxy;

        public MfaService(
            IMfaProxy mfaProxy,
            ILogger<MfaService> logger)
        {
            _mfaProxy = mfaProxy;
            _logger = logger;
        }

        public async Task<MfaSmsDataResponse> GetMfaSmsDataAsync(long consumerId)
        {
            LogInformation(
                nameof(GetMfaSmsDataAsync),
                $"Start {nameof(GetMfaSmsDataAsync)} with ConsumerId: {consumerId}");
            try
            {
                return await _mfaProxy.GetMfaSmsDataAsync(consumerId);
            }
            catch (ApiException rex)
            {
                if (rex.StatusCode == HttpStatusCode.NotFound)
                {
                    LogInformation(
                        nameof(GetMfaSmsDataAsync),
                        $"Finished {nameof(GetMfaSmsDataAsync)} of ConsumerId: {consumerId}. Customer details not found.");

                    return null;
                }

                LogError(rex,
                    nameof(GetMfaSmsDataAsync),
                    $"Failed to {nameof(GetMfaSmsDataAsync)} of ConsumerId: {consumerId}. " +
                    $"Refit API exception with message: {rex.Message}");

                throw new MfaApiException(rex.Message, rex);
            }
            catch (Exception ex)
            {
                LogError(ex,
                    nameof(GetMfaSmsDataAsync),
                    $"Failed to {nameof(GetMfaSmsDataAsync)} of ConsumerId: {consumerId} with message: {ex.Message}");

                throw;
            }
        }

        private void LogInformation(string methodName, string message)
        {
            _logger.LogInformation($"{SerilogProperty.ClassName} :: {SerilogProperty.MethodName} :: {SerilogProperty.Message}",
                nameof(MfaService),
                methodName,
                message);
        }

        private void LogError(Exception ex, string methodName, string message)
        {
            _logger.LogError(ex,
                $"{SerilogProperty.ClassName} :: {SerilogProperty.MethodName} :: {SerilogProperty.Message}",
                nameof(MfaService),
                methodName,
                message);
        }
    }
}