using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;
using Zip.Services.Accounts.Contract.Order;

namespace Zip.Api.CustomerSummary.Application.Orders.Command
{
    public class CancelOrderInstallmentsCommandHandler : IRequestHandler<CancelOrderInstallmentsCommand>
    {
        private readonly IAccountsService _accountsService;

        private readonly ILogger<CancelOrderInstallmentsCommandHandler> _logger;

        public CancelOrderInstallmentsCommandHandler(
            IAccountsService accountsService,
            ILogger<CancelOrderInstallmentsCommandHandler> logger)
        {
            _accountsService = accountsService;
            _logger = logger;
        }

        public async Task<Unit> Handle(CancelOrderInstallmentsCommand request, CancellationToken cancellationToken)
        {
            var logSuffix = $"Request: {request.ToJsonString()}";

            try
            {
                LogInformation($"Start cancelling order installments for {logSuffix}");

                await _accountsService.TransferOrderBalance(request.AccountId,
                                                            request.OrderId,
                                                            new TransferOrderRequest
                                                            {
                                                                TransferType = TransferType.ToInterestBearing
                                                            });

                LogInformation($"Finished cancelling order installments for {logSuffix}");

                return Unit.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                                 $"{SerilogProperty.ClassName}::{SerilogProperty.MethodName}::{SerilogProperty.Message}",
                                 nameof(CancelOrderInstallmentsCommandHandler),
                                 nameof(Handle),
                                 $"Failed to cancel order installments for {logSuffix}");

                throw;
            }
        }

        private void LogInformation(string message)
        {
            _logger.LogInformation(
                $"{SerilogProperty.ClassName}::{SerilogProperty.MethodName}::{SerilogProperty.Message}",
                nameof(CancelOrderInstallmentsCommandHandler),
                nameof(Handle),
                message);
        }
    }
}
