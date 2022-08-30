using WeLearn.Api.Dtos.Comment;
using WeLearn.Data.Models;
using WeLearn.Shared.Extensions.Models;

namespace WeLearn.Api.Extensions.Models;

public static class CommentExtensions
{
    public static GetCommentDto MapToGetDto(this Comment c)
    {
        return new GetCommentDto
        {
            Id = c.Id,
            CreatedDate = c.CreatedDate,
            UpdatedDate = c.UpdatedDate,
            Body = c.Body,
            AuthorId = c.AuthorId,
            ContentId = c.ContentId,
            Author = c.Author.MapToGetDto()
        };
    }
}
