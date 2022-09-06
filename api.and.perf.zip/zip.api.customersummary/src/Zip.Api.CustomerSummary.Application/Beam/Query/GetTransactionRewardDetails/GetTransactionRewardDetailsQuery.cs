using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Beam;

namespace Zip.Api.CustomerSummary.Application.Beam.Query.GetTransactionRewardDetails
{
    public class GetTransactionRewardDetailsQuery : IRequest<TransactionRewardDetailsResponse>
    {
        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public long TransactionId { get; set; }
    }
}
