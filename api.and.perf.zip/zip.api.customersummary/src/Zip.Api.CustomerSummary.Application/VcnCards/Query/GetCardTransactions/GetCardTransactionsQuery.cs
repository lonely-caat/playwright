using System;
using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zip.Api.CustomerSummary.Domain.Entities.VcnCard;

namespace Zip.Api.CustomerSummary.Application.VcnCards.Query.GetCardTransactions
{
    public class GetCardTransactionsQuery : IRequest<IEnumerable<CardTransaction>>
    {
        public GetCardTransactionsQuery(Guid cardId)
        {
            CardId = cardId;
        }

        public GetCardTransactionsQuery()
        {
        }

        [FromRoute(Name = "cardId")]
        public Guid CardId { get; set; }
    }
}
