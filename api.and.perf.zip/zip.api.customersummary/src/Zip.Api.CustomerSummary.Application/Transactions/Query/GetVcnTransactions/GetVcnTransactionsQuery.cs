using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zip.Api.CustomerSummary.Domain.Entities.VcnCard;

namespace Zip.Api.CustomerSummary.Application.Transactions.Query.GetVcnTransactions
{
    public class GetVcnTransactionsQuery : IRequest<IEnumerable<CardTransaction>>
    {
        public GetVcnTransactionsQuery()
        {
        }

        public GetVcnTransactionsQuery(string networkReferenceId)
        {
            NetworkReferenceId = networkReferenceId;
        }

        [FromQuery(Name = "nrid")]
        public string NetworkReferenceId { get; set; }
    }
}
