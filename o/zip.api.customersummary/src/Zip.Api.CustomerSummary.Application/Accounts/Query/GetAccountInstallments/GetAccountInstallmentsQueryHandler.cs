using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Models;

namespace Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInstallments
{
    public class GetAccountInstallmentsQueryHandler : IRequestHandler<GetAccountInstallmentsQuery, OrdersResponse>
    {
        private readonly IAccountsService _accountsService;

        private readonly ILogger<GetAccountInstallmentsQueryHandler> _logger;
        
        public GetAccountInstallmentsQueryHandler(
            IAccountsService accountsService,
            ILogger<GetAccountInstallmentsQueryHandler> logger)
        {
            _accountsService = accountsService;
            _logger = logger;
        }

        public async Task<OrdersResponse> Handle(GetAccountInstallmentsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                LogInformation($"Start getting installments for AccountId {request.AccountId}");

                var response = await _accountsService.GetOrders(request.AccountId);
                
                LogInformation($"Finish getting installments for AccountId {request.AccountId}");

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                                 $"{SerilogProperty.ClassName} :: {SerilogProperty.MethodName} : {SerilogProperty.Message}",
                                 nameof(GetAccountInstallmentsQueryHandler),
                                 nameof(Handle),
                                 $"Failed to get installments for AccountId {request.AccountId}");

                throw;
            }
        }

        private void LogInformation(string message)
        {
            _logger.LogInformation(
                $"{SerilogProperty.ClassName}::{SerilogProperty.MethodName}::{SerilogProperty.Message}",
                nameof(GetAccountInstallmentsQueryHandler),
                nameof(Handle),
                message);
        }
    }
}