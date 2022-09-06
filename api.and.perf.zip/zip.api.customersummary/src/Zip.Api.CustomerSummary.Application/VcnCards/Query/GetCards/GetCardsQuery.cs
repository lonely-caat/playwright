using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zip.Api.CustomerSummary.Domain.Entities.VcnCard;

namespace Zip.Api.CustomerSummary.Application.VcnCards.Query.GetCards
{
    public class GetCardsQuery : IRequest<RootCards>
    {
        public GetCardsQuery(Guid customerId, long accountId)
        {
            CustomerId = customerId;
            AccountId = accountId;
        }

        public GetCardsQuery(Guid customerId)
        {
            CustomerId = customerId;
        }

        public GetCardsQuery() { }
        
        [FromRoute(Name = "customerId")]
        public Guid CustomerId { get; set; }
        
        [FromQuery(Name = "accountId")]
        public long? AccountId { get; set; }
    }
}
