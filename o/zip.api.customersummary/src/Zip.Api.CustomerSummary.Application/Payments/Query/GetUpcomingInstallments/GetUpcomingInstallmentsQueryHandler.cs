using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Payments.Query.GetUpcomingInstallments
{
    public class GetUpcomingInstallmentsQueryHandler : IRequestHandler<GetUpcomingInstallmentsQuery, GetUpcomingInstallmentsResponse>
    {
        private readonly ILogger<GetUpcomingInstallmentsQueryHandler> _logger;

        private readonly ICoreGraphService _coreGraphService;
        
        public GetUpcomingInstallmentsQueryHandler(
            ILogger<GetUpcomingInstallmentsQueryHandler> logger,
            ICoreGraphService coreGraphService)
        {
            _logger = logger;
            _coreGraphService = coreGraphService;
        }

        public async Task<GetUpcomingInstallmentsResponse> Handle(GetUpcomingInstallmentsQuery request, CancellationToken cancellationToken)
        {
            var logSuffix = $"for Request {request.ToJsonString()}";
            try
            {
                LogInformation($"Start processing {nameof(GetUpcomingInstallmentsQuery)} {logSuffix}");

                var response =
                   await _coreGraphService.GetUpcomingInstallmentsAsync(request.AccountId, request.CustomerId, request.ToDate);

                LogInformation($"Finished processing {nameof(GetUpcomingInstallmentsQuery)} {logSuffix}");

                return new GetUpcomingInstallmentsResponse {
                    Installments = response
                };
            }
            catch (Exception ex)
            {
                LogError(ex, $"Failed to process {nameof(GetUpcomingInstallmentsQuery)} with message {ex.Message} {logSuffix}");

                throw;
            }
        }
        
        private void LogInformation(string message)
        {
            _logger.LogInformation(
                $"{SerilogProperty.ClassName} :: {SerilogProperty.MethodName} :: {SerilogProperty.Message}",
                nameof(GetUpcomingInstallmentsQueryHandler),
                nameof(Handle),
                message);
        }
        
        private void LogError(Exception ex, string message)
        {
            _logger.LogError(ex,
                $"{SerilogProperty.ClassName} :: {SerilogProperty.MethodName} :: {SerilogProperty.Message}",
                nameof(GetUpcomingInstallmentsQueryHandler),
                nameof(Handle),
                message);
        }
    }
}
