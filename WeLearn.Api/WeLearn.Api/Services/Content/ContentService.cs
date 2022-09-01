using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WeLearn.Api.Dtos.Content;
using WeLearn.Api.Extensions.Models;
using WeLearn.Data.Models.Content.Notices;
using WeLearn.Data.Persistence;
using WeLearn.Shared.Dtos.Paging;
using WeLearn.Shared.Exceptions.Models;
using WeLearn.Shared.Extensions.Paging;

namespace WeLearn.Api.Services.Content;

public class ContentService : IContentService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger _logger;

    public ContentService(
        ApplicationDbContext dbContext,
        ILogger<ContentService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    // TODO rename or accept argument by which types of content are filtered
    public async Task<PagedResponseDto<GetContentDto>> GetCourseContentAsync(Guid courseId, PageOptionsDto pageOptions)
    {
        _logger.LogInformation("Getting course content for course with id {courseId}", courseId);

        var course = await _dbContext.Courses.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == courseId);
        if (course is null)
            throw new CourseNotFoundException();

        var dtos = await _dbContext.Contents
            .AsNoTracking()
            .Include(c => c.Creator)
                .ThenInclude(a => a.User)
            .Include(c => c.ExternalSystem)
            .Include(c => c.Comments)
            .Where(c =>
            !(c is WeLearn.Data.Models.Content.Document)
            && !(c is WeLearn.Data.Models.Content.StudyMaterial)
            && !(c is StudyYearNotice))
            .Where(c => c.CourseId == courseId)
            .OrderByDescending(c => c.UpdatedDate)
            .Select(c => c.WithCommentCount())
            .GetPagedResponseDtoAsync(pageOptions, MapContentToDto());

        return dtos;
    }

    private static Expression<Func<WeLearn.Data.Models.Content.Content, GetContentDto>> MapContentToDto()
    {
        return c => c.MapToGetDto();
    }
}
