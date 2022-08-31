using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WeLearn.Api.Dtos.Comment;
using WeLearn.Api.Exceptions.Models;
using WeLearn.Api.Extensions.Models;
using WeLearn.Data.Persistence;
using WeLearn.Shared.Dtos.Paging;
using WeLearn.Shared.Exceptions.Models;
using WeLearn.Shared.Extensions.Paging;

namespace WeLearn.Api.Services.Comment;

public class CommentService : ICommentService
{
    private readonly ApplicationDbContext _dbContext;

    public CommentService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResponseDto<GetCommentDto>> GetContentCommentsAsync(Guid contentId, PageOptionsDto pageOptions)
    {
        var contentExists = await _dbContext.Contents.AnyAsync(c => c.Id == contentId);
        if (!contentExists)
            throw new ContentNotFoundException();

        var dtos = await _dbContext.Comments
            .AsNoTracking()
            .Include(c => c.Author)
                .ThenInclude(c => c.User)
            .Include(c => c.Content)
            .Where(c => c.ContentId == contentId)
            .OrderByDescending(c => c.UpdatedDate)
            .GetPagedResponseDtoAsync(pageOptions, MapCourseToGetDto());

        return dtos;
    }

    public async Task<GetCommentDto> CreateCommentAsync(
        string body,
        Guid authorId,
        Guid contentId)
    {
        var author = await _dbContext.Accounts
            .Include(a => a.User)
            .FirstOrDefaultAsync(u => u.Id == authorId);
        if (author is null)
            throw new AccountNotFoundException();

        var comment = new WeLearn.Data.Models.Comment(body, authorId, contentId) { Author = author };
        _dbContext.Add(comment);
        await _dbContext.SaveChangesAsync();

        return comment.MapToGetDto();
    }

    public async Task<GetCommentDto> DeleteCommentAsync(Guid commentId)
    {
        var comment = await _dbContext.Comments
            .Include(c => c.Author)
                .ThenInclude(a => a.User)
            .FirstOrDefaultAsync(c => c.Id == commentId);
        if (comment == null)
            throw new CommentNotFoundException();

        _dbContext.Remove(comment);
        await _dbContext.SaveChangesAsync();
        return comment.MapToGetDto();
    }

    private static Expression<Func<WeLearn.Data.Models.Comment, GetCommentDto>> MapCourseToGetDto()
    {
        return c => c.MapToGetDto();
    }
}
