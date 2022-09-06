using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Refit;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreService.Models;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.CoreService
{
    public class CoreService : ICoreService
    {
        private readonly IOptions<CoreApiProxyOptions> _coreApiProxyOptions;

        private readonly ICoreServiceProxy _coreServiceProxy;

        private readonly ILogger<CoreService> _logger;

        public CoreService(
            ICoreServiceProxy coreServiceProxy,
            ILogger<CoreService> logger,
            IOptions<CoreApiProxyOptions> coreApiProxyOptions)
        {
            _coreServiceProxy = coreServiceProxy;
            _logger = logger;
            _coreApiProxyOptions = coreApiProxyOptions;
        }

        public async Task<GetCoreTokenResponse> GetCoreTokenAsync(CancellationToken cancellationToken)
        {
            try
            {
                LogInformation("Fetching the Core's token",
                               nameof(GetCoreTokenAsync));

                var data = new Dictionary<string, string>
                {
                    { "grant_type", "client_credentials" },
                    { "client_id", _coreApiProxyOptions.Value.ClientId },
                    { "client_secret", _coreApiProxyOptions.Value.ClientSecret }
                };
                
                LogInformation("Successfully obtained token for core's token for reset password",
                               nameof(GetCoreTokenAsync));
                return await _coreServiceProxy.GetTokenAsync(data, cancellationToken);
            }
            catch (ApiException ex)
            {
                var errorMessage = $"Failed to retrieve access token: {ex.ToJsonString()}";
                LogError(errorMessage,
                         $"{nameof(GetCoreTokenAsync)}",
                         ex);

                throw new CoreApiException(errorMessage, ex);
            }
        }

        public async Task<HttpResponseMessage> SendResetPasswordEmailAsync(
            CoreResetPasswordModel coreResetPassword,
            string token,
            CancellationToken cancellationToken)
        {
            LogInformation($"Attempting to send reset password to email: {coreResetPassword.Email}",
                           nameof(SendResetPasswordEmailAsync));
            var response = await _coreServiceProxy.SendResetPasswordEmailAsync(coreResetPassword, token, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = $"Failed to send reset password email to {coreResetPassword.Email}";
                LogError(errorMessage,
                         $"{nameof(SendResetPasswordEmailAsync)}",
                         new CoreApiException(response.ToJsonString()));
                throw new CoreApiException(errorMessage);
            }
            
            LogInformation($"Successfully sent reset password to email: {coreResetPassword.Email}",
                nameof(SendResetPasswordEmailAsync));
            return response;
        }

        private void LogInformation(string message, string method)
        {
            _logger.LogInformation(
                $"{SerilogProperty.ClassName} :: {SerilogProperty.MethodName} :: {SerilogProperty.Message}",
                nameof(CoreService),
                method,
                message
            );
        }

        private void LogError(string message, string method, Exception ex)
        {
            _logger.LogError( ex,
                $"{SerilogProperty.ClassName} :: {SerilogProperty.MethodName} :: {SerilogProperty.Message}",
                nameof(CoreService),
                method,
                message
            );
        }
    }
}