using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Zip.Api.CustomerSummary.Domain.Entities.Transactions;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Transactions.Query.GetTransactionHistory
{
    public class GetTransactionHistoryHandler : IRequestHandler<GetTransactionHistoryQuery, IEnumerable<TransactionHistory>>
    {
        private readonly ITransactionHistoryContext _transactionHistoryContext;

        private readonly ILogger<GetTransactionHistoryHandler> _logger;

        public GetTransactionHistoryHandler(
            ITransactionHistoryContext transactionHistoryContext,
            ILogger<GetTransactionHistoryHandler> logger)
        {
            _transactionHistoryContext = transactionHistoryContext;
            _logger = logger;
        }

        public async Task<IEnumerable<TransactionHistory>> Handle(GetTransactionHistoryQuery request, CancellationToken cancellationToken)
        {
            if (!request.StartDate.HasValue)
            {
                request.StartDate = DateTime.Now.AddMonths(-6);
            }

            if (!request.EndDate.HasValue)
            {
                request.EndDate = DateTime.Now.AddDays(1);
            }

            _logger.LogInformation($"{SerilogProperty.ClassName} :: {SerilogProperty.MethodName} :: {SerilogProperty.Message}",
                nameof(GetTransactionHistoryHandler),
                nameof(Handle),
                $"Start getting TransactionHistory for {request.ToJsonString()}");

            var transactions = await _transactionHistoryContext.FindByConsumerIdAsync(request.ConsumerId, request.StartDate.Value, request.EndDate.Value);

            _logger.LogInformation($"{SerilogProperty.ClassName} :: {SerilogProperty.MethodName} :: {SerilogProperty.Message}",
                nameof(GetTransactionHistoryHandler),
                nameof(Handle),
                $"Finished getting TransactionHistory for {request.ToJsonString()} with {transactions.Count()} results");

            return transactions;
        }
    }
}
