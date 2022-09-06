using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Services.StatementService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.StatementService.Models;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.StatementService
{
    public class StatementsService : IStatementsService
    {
        private readonly IStatementsApiProxy _statementsApiProxy;

        public StatementsService(IStatementsApiProxy statementsApiProxy)
        {
            _statementsApiProxy = statementsApiProxy;
        }

        public async Task<bool> GenerateStatementsAsync(GenerateStatementsRequest request, CancellationToken cancellationToken)
        {
            var logSuffix = $"for accountId::{request.Accounts.FirstOrDefault()} of statementDate::{request.StatementDate}";

            try
            {
                Log.Information("{class} :: {action} : {message}",
                                nameof(StatementsService),
                                nameof(GenerateStatementsAsync),
                                $"Start sending request to StatementsApi to generate statement {logSuffix}");
                
                using var httpResponseMessage = await _statementsApiProxy.TriggerStatementsGenerationAsync(request, cancellationToken);

                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    throw new StatementsApiException(
                        $"Failed to generate statement {logSuffix}, StatusCode::{httpResponseMessage.StatusCode}");
                }
                
                Log.Information("{class} :: {action} : {message}",
                                nameof(StatementsService),
                                nameof(GenerateStatementsAsync),
                                $"Successfully generated statement from StatementsApi {logSuffix}");

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{class} :: {action} : {message}",
                          nameof(StatementsService),
                          nameof(GenerateStatementsAsync),
                          ex.Message);

                return false;
            }
        }
    }
}
