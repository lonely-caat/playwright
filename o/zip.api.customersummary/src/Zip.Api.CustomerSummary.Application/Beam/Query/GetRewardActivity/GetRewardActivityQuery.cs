using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using Zip.Api.CustomerSummary.Domain.Common;
using Zip.Api.CustomerSummary.Domain.Common.Constants;
using Zip.Api.CustomerSummary.Domain.Entities.Beam;

namespace Zip.Api.CustomerSummary.Application.Beam.Query.GetRewardActivity
{
    public class GetRewardActivityQuery : IRequest<Pagination<RewardActivity>>
    {
        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public long PageNumber { get; set; }

        public long PageSize { get; set; } = PaginationDefault.PageSize;

        public string Region { get; set; } = Regions.Australia;

        public GetRewardActivityQuery(Guid customerId, long pageNumber, long? pageSize = PaginationDefault.PageSize, string region = Regions.Australia)
        {
            CustomerId = customerId;
            PageNumber = pageNumber;
            PageSize = pageSize.HasValue ? pageSize.Value : PaginationDefault.PageSize;
            Region = region ?? Regions.Australia;
        }
    }
}
