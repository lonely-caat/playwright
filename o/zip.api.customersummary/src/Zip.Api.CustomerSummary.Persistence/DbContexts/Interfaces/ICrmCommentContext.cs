using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Common;
using Zip.Api.CustomerSummary.Domain.Entities.Comments;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces
{
    public interface ICrmCommentContext
    {
        Task<Pagination<CommentDto>> GetCommentsAsync(long consumerId, CommentCategory? category, CommentType? type, long pageIndex = 1, long pageSize = 100);

        Task<CommentDto> CreateAsync(long consumerId, CommentCategory category, CommentType type, string detail, string commentBy);

        Task<CommentDto> GetAsync(long id);
    }
}