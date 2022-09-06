using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using Zip.Api.CustomerSummary.Domain.Common.Constants;
using Zip.Api.CustomerSummary.Domain.Entities.Beam;

namespace Zip.Api.CustomerSummary.Application.Beam.Query.GetCustomerDetails
{
    public class GetCustomerDetailsQuery : IRequest<CustomerDetails>
    {
        [Required]
        public Guid CustomerId { get; set; }

        public string Region { get; set; } = Regions.Australia;

        public GetCustomerDetailsQuery(Guid customerId, string region = Regions.Australia)
        {
            CustomerId = customerId;
            Region = region ?? Regions.Australia;
        }

        public GetCustomerDetailsQuery()
        {
        }
    }
}
