using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Models;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService
{
    public class CommunicationsService : ICommunicationsService
    {
        private readonly ICommunicationsServiceProxy _communicationApiProxy;
        private readonly ILogger<CommunicationsService> _logger;

        public CommunicationsService(ICommunicationsServiceProxy communicationApiProxy,
            ILogger<CommunicationsService> logger)
        {
            _communicationApiProxy = communicationApiProxy ?? throw new ArgumentNullException(nameof(communicationApiProxy));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CommunicationApiResponse> SendCloseAccountAsync(CloseAccount request)
        {
            _logger.LogInformation($"{SerilogProperty.ClassName} :: {SerilogProperty.MethodName} :: {SerilogProperty.Message}",
                nameof(CommunicationsService),
                nameof(SendCloseAccountAsync),
                $"Attempting to {nameof(SendCloseAccountAsync)} to email {request.Email}");
            try
            {
                var communicationApiResponse = await _communicationApiProxy.SendCloseAccountEmailAsync(request);
                _logger.LogInformation($"{SerilogProperty.ClassName} :: {SerilogProperty.MethodName} :: {SerilogProperty.Message}",
                    nameof(CommunicationsService),
                    nameof(SendCloseAccountAsync),
                    $"Finished {nameof(SendCloseAccountAsync)} to email {request.Email} with response {communicationApiResponse.Success} and message {communicationApiResponse.Message}");
                return communicationApiResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    $"{SerilogProperty.ClassName} :: {SerilogProperty.MethodName} :: {SerilogProperty.Message}",
                    nameof(SendCloseAccountAsync),
                    nameof(CommunicationsService),
                    ex.Message);
                throw;
            }
        }

        public async Task<CommunicationApiResponse> SendPaidOutCloseEmailAsync(PaidOutAndClosedEmail request)
        {
            _logger.LogInformation($"{SerilogProperty.ClassName} :: {SerilogProperty.MethodName} :: {SerilogProperty.Message}",
                nameof(CommunicationsService),
                nameof(SendPaidOutCloseEmailAsync),
                $"Attempting to {nameof(SendPaidOutCloseEmailAsync)} to consumer {request.ConsumerId}");
            try
            {
                var communicationApiResponse =  await _communicationApiProxy.SendPaidOutAndClosedEmailAsync(request);
                _logger.LogInformation($"{SerilogProperty.ClassName} :: {SerilogProperty.MethodName} :: {SerilogProperty.Message}",
                    nameof(CommunicationsService),
                    nameof(SendPaidOutCloseEmailAsync),
                    $"Finished {nameof(SendPaidOutCloseEmailAsync)} to consumer {request.ConsumerId} with response {communicationApiResponse.Success} and message {communicationApiResponse.Message}");
                return communicationApiResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    $"{SerilogProperty.ClassName} :: {SerilogProperty.MethodName} :: {SerilogProperty.Message}",
                          nameof(CommunicationsService),
                          nameof(SendPaidOutCloseEmailAsync),
                          ex.Message);
                throw;
            }
        }

        public async Task<CommunicationApiResponse> SendResetPasswordAsync(ResetPassword request)
        {
            _logger.LogInformation($"{SerilogProperty.ClassName} :: {SerilogProperty.MethodName} :: {SerilogProperty.Message}",
                nameof(CommunicationsService),
                nameof(SendResetPasswordAsync),
                $"Attempting to {nameof(SendResetPasswordAsync)} to email {request.Email}");
            try
            {
                var communicationApiResponse = await _communicationApiProxy.SendResetPasswordEmailAsync(request);
                _logger.LogInformation($"{SerilogProperty.ClassName} :: {SerilogProperty.MethodName} :: {SerilogProperty.Message}",
                    nameof(CommunicationsService),
                    nameof(SendResetPasswordAsync),
                    $"Finished {nameof(SendResetPasswordAsync)} to email {request.Email} with response {communicationApiResponse.Success} and message {communicationApiResponse.Message}");
                return communicationApiResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    $"{SerilogProperty.ClassName} :: {SerilogProperty.MethodName} :: {SerilogProperty.Message}",
                    nameof(CommunicationsService),
                    nameof(SendResetPasswordAsync),
                    ex.Message);
                throw;
            }
        }
    }
}