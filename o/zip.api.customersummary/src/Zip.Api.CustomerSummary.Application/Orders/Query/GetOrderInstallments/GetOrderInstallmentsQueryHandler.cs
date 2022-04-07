using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Models;

namespace Zip.Api.CustomerSummary.Application.Orders.Query.GetOrderInstallments
{
    public class GetOrderInstallmentsQueryHandler : IRequestHandler<GetOrderInstallmentsQuery, OrderDetailResponse>
    {
        private readonly IAccountsService _accountsService;

        private readonly ILogger<GetOrderInstallmentsQueryHandler> _logger;


        public GetOrderInstallmentsQueryHandler(
            IAccountsService accountsService,
            ILogger<GetOrderInstallmentsQueryHandler> logger)
        {
            _accountsService = accountsService;
            _logger = logger;
        }

        public async Task<OrderDetailResponse> Handle(
            GetOrderInstallmentsQuery request,
            CancellationToken cancellationToken)
        {
            var logSuffix = $"for Request {request.ToJsonString()}";

            try
            {
                LogInformation($"Start getting order installments {logSuffix}");

                var response = await _accountsService.GetOrderDetail(request.AccountId, request.OrderId);
                
                LogInformation($"Finish getting order installments {logSuffix}");

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                                 $"{SerilogProperty.ClassName}::{SerilogProperty.MethodName}::{SerilogProperty.Message}",
                                 nameof(GetOrderInstallmentsQueryHandler),
                                 nameof(Handle),
                                 $"Failed to get order installments {logSuffix}");
                throw;
            }
        }

        private void LogInformation(string message)
        {
            _logger.LogInformation(
                $"{SerilogProperty.ClassName}::{SerilogProperty.MethodName}::{SerilogProperty.Message}",
                nameof(GetOrderInstallmentsQueryHandler),
                nameof(Handle),
                message);
        }
    }
}