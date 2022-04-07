using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;

namespace Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.Vcn
{
    public class GetConsumerForVcnQuery : IRequest<Consumer>
    {
        [FromQuery(Name = "customerId")]
        public Guid CustomerId { get; set; }

        [FromQuery(Name = "product")]
        public string Product { get; set; }

        public GetConsumerForVcnQuery(Guid customerId, string product)
        {
            CustomerId = customerId;
            Product = product;
        }

        public GetConsumerForVcnQuery()
        {
        }
    }
}
