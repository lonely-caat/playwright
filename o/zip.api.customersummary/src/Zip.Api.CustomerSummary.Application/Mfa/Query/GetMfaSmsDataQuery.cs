using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Mfa;

namespace Zip.Api.CustomerSummary.Application.Mfa.Query
{
    [ExcludeFromCodeCoverage]
    public class GetMfaSmsDataQuery : IRequest<MfaSmsDataResponse>
    {
        [Required]
        public long ConsumerId { get; set; }

        public GetMfaSmsDataQuery(long consumerId)
        {
            ConsumerId = consumerId;
        }

        public GetMfaSmsDataQuery()
        {
        }
    }
}