
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Serilog;
using Zip.Api.CustomerSummary.Domain.Entities.VcnCard;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.PaymentWebhookService.Interfaces;

namespace Zip.Api.CustomerSummary.Application.VcnCards.Query.GetCardTransactions
{
    public class GetCardTransactionsQueryHandler : IRequestHandler<GetCardTransactionsQuery, IEnumerable<CardTransaction>>
    {
        private readonly IPaymentWebhookService _paymentWebhookService;

        public GetCardTransactionsQueryHandler(IPaymentWebhookService paymentWebhookService)
        {
            _paymentWebhookService = paymentWebhookService;
        }

        public async Task<IEnumerable<CardTransaction>> Handle(GetCardTransactionsQuery request,
            CancellationToken cancellationToken)
        {
            Log.Information("{class} :: {action} : {message}",
                nameof(GetCardTransactionsQueryHandler),
                nameof(Handle),
                $"Start getting transactions for card of CardId::{request.CardId}");

            var cardTransactions = await _paymentWebhookService.GetCardTransactionsAsync(request.CardId, cancellationToken);

            Log.Information("{class} :: {action} : {message}",
                nameof(GetCardTransactionsQueryHandler),
                nameof(Handle),
                $"Successfully retrieved transactions for card of CardId::{request.CardId}");

            return cardTransactions;
        }
    }
}
