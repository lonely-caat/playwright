using Zip.Api.CustomerSummary.Domain.Entities.Comments;

namespace Zip.Api.CustomerSummary.Domain.Entities.Dto
{
    public class CommentDto
    {
        public long Id { get; set; }

        public CommentType Type { get; set; }

        public string TypeString => Type.ToString();

        public CommentCategory Category { get; set; }

        public string CategoryString => Category.ToString();

        public long ReferenceId { get; set; }

        public string Detail { get; set; }

        public System.DateTime TimeStamp { get; set; }

        public string CommentBy { get; set; }
    }
}
