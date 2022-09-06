using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Serilog;
using Zip.Api.CustomerSummary.Domain.Entities.VcnCard;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.PaymentWebhookService.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Transactions.Query.GetVcnTransactions
{
    public class GetVcnTransactionsQueryHandler : IRequestHandler<GetVcnTransactionsQuery, IEnumerable<CardTransaction>>
    {
        private readonly IPaymentWebhookService _paymentWebhookService;

        public GetVcnTransactionsQueryHandler(IPaymentWebhookService paymentWebhookService)
        {
            _paymentWebhookService = paymentWebhookService ?? throw new ArgumentNullException(nameof(paymentWebhookService));
        }

        public async Task<IEnumerable<CardTransaction>> Handle(GetVcnTransactionsQuery request, CancellationToken cancellationToken)
        {
            Log.Information("{class} :: {action} : {message}",
                            nameof(GetVcnTransactionsQueryHandler),
                            nameof(Handle),
                            $"Start getting VcnTransactions of NetworkReferenceId::{request.NetworkReferenceId}");

            var rv = await _paymentWebhookService.GetTransactionsByNetworkReferenceIdAsync(request.NetworkReferenceId, cancellationToken);
            Log.Information("{class} :: {action} : {message}",
                        nameof(GetVcnTransactionsQueryHandler),
                        nameof(Handle),
                        $"Successfully retrieved VcnTransactions of NetworkReferenceId::{request.NetworkReferenceId}");
            return rv;
        }
    }
}
