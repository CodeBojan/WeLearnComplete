
namespace WeLearn.Api.Dtos.Comment
{
    public class PostCommentDto
    {
        public string Body { get; set; }
        public Guid AuthorId { get; set; }
        public Guid ContentId { get; set; }
    }
}
