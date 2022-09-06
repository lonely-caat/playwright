using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Comments;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;

namespace Zip.Api.CustomerSummary.Application.Comments.Command.CreateCrmComment
{
    public class CreateCrmCommentCommand : IRequest<CommentDto>
    {
        public long ReferenceId { get; set; }

        public string CommentBy { get; set; }

        public CommentCategory? Category { get; set; }

        public CommentType? Type { get; set; }

        public string Detail { get; set; }
    }
}
