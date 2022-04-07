using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Request.PayOrder;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Response.PayOrder;

namespace Zip.Api.CustomerSummary.Application.Payments.Command.PayOrder
{
    public class PayOrderCommandHandler : IRequestHandler<PayOrderCommand, PayOrderInnerResponse>
    {
        private readonly ICoreGraphService _coreGraphService;

        private readonly ILogger<PayOrderCommandHandler> _logger;

        public PayOrderCommandHandler(
            ICoreGraphService coreGraphService,
            ILogger<PayOrderCommandHandler> logger)
        {
            _coreGraphService = coreGraphService;
            _logger = logger;
        }

        public async Task<PayOrderInnerResponse> Handle(PayOrderCommand request, CancellationToken cancellationToken)
        {
            var logSuffix = $"for Request: {request.ToJsonString()}";

            try
            {
                LogInformation($"Start processing {nameof(PayOrderCommand)} {logSuffix}");

                var payOrderInput =
                    new PayOrderInput(request.AccountId.ToString(),
                                      request.CustomerId,
                                      request.OrderId,
                                      request.Amount);

                var result = await _coreGraphService.PayOrderAsync(payOrderInput);

                LogInformation($"Finish processing {nameof(PayOrderCommand)} {logSuffix}");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                                 $"{SerilogProperty.ClassName}::{SerilogProperty.MethodName}::{SerilogProperty.Message}",
                                 nameof(PayOrderCommandHandler),
                                 nameof(Handle),
                                 $"Failed to process {nameof(PayOrderCommand)} {logSuffix}");

                throw;
            }
        }

        private void LogInformation(string message)
        {
            _logger.LogInformation(
                $"{SerilogProperty.ClassName}::{SerilogProperty.MethodName}::{SerilogProperty.Message}",
                nameof(PayOrderCommandHandler),
                nameof(Handle),
                message);
        }
    }
}