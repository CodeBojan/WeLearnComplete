using WeLearn.Api.Dtos.Comment;
using WeLearn.Shared.Dtos.Paging;

namespace WeLearn.Api.Services.Comment
{
    public interface ICommentService
    {
        Task<GetCommentDto> CreateCommentAsync(string body, Guid authorId, Guid contentId);
        Task<GetCommentDto> DeleteCommentAsync(Guid commentId);
        Task<PagedResponseDto<GetCommentDto>> GetContentCommentsAsync(Guid contentId, PageOptionsDto pageOptions);
    }
}