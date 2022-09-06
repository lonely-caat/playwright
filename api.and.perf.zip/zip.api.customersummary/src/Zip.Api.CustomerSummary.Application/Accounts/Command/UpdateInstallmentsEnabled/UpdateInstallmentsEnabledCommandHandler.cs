using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;
using Zip.Services.Accounts.Contract.Account;

namespace Zip.Api.CustomerSummary.Application.Accounts.Command.UpdateInstallmentsEnabled
{
    public class UpdateInstallmentsEnabledCommandHandler : IRequestHandler<UpdateInstallmentsEnabledCommand, AccountResponse>
    {
        private readonly IAccountsService _accountsService;

        private readonly ILogger<UpdateInstallmentsEnabledCommandHandler> _logger;

        public UpdateInstallmentsEnabledCommandHandler(
            IAccountsService accountsService,
            ILogger<UpdateInstallmentsEnabledCommandHandler> logger)
        {
            _accountsService = accountsService;
            _logger = logger;
        }
        
        public async Task<AccountResponse> Handle(UpdateInstallmentsEnabledCommand request, CancellationToken cancellationToken)
        {
            try
            {
                LogInformation($"Start setting installments enabled to {request.IsInstallmentsEnabled} for AccountId: {request.AccountId}");

                var updateConfigurationRequest = new UpdateConfigurationRequest()
                {
                    AccountId = request.AccountId,
                    AccountTypeId = request.AccountTypeId,
                    Configuration = new UpdateAccountConfigurationRequest()
                    {
                        InstallmentAccount = request.IsInstallmentsEnabled
                    }
                };
                
                var response = await _accountsService.UpdateConfiguration(updateConfigurationRequest);

                LogInformation($"Finish setting installments enabled to {request.IsInstallmentsEnabled} for AccountId: {request.AccountId}");

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    $"{SerilogProperty.ClassName}::{SerilogProperty.MethodName}::{SerilogProperty.Message}",
                    nameof(UpdateInstallmentsEnabledCommandHandler),
                    nameof(Handle),
                    $"Failed to set installments enabled to {request.IsInstallmentsEnabled} for AccountId: {request.AccountId}");

                throw;
            }
        }
        
        private void LogInformation(string message)
        {
            _logger.LogInformation(
                $"{SerilogProperty.ClassName}::{SerilogProperty.MethodName}::{SerilogProperty.Message}",
                nameof(UpdateInstallmentsEnabledCommandHandler),
                nameof(Handle),
                message);
        }
    }
}
