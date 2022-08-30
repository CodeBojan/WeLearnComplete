using WeLearn.Shared.Dtos.Account;

namespace WeLearn.Api.Dtos.Comment
{
    public class GetCommentDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Body { get; set; }
        public Guid AuthorId { get; set; }
        public Guid ContentId { get; set; }

        public GetAccountDto Author { get; set; }
        // TODO content
    }
}
