using Zip.Api.CustomerSummary.Domain.Entities.Comments;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Crm.Models
{
    public class CreateCommentRequest
    {
        public long ReferenceId { get; set; }

        public string CommentBy { get; set; }

        public CommentCategory Category { get; set; }

        public CommentType Type { get; set; }

        public string Detail { get; set; }
    }
}