using MediatR;
using Zip.Api.CustomerSummary.Domain.Common;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;

namespace Zip.Api.CustomerSummary.Application.Comments.Query.GetCrmComments
{
    public class GetCrmCommentsQuery : IRequest<Pagination<CommentDto>>
    {
        public long ConsumerId { get; }
        public long PageIndex { get; } = 1;
        public long PageSize { get; } = 100;

        public GetCrmCommentsQuery(long consumerId, long pageIndex = 1, long pageSize = 100)
        {
            ConsumerId = consumerId;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}
