using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WeLearn.Api.Dtos.Notice;
using WeLearn.Api.Extensions.Models;
using WeLearn.Data.Models.Content.Notices;
using WeLearn.Data.Persistence;
using WeLearn.Shared.Dtos.Paging;
using WeLearn.Shared.Exceptions.Models;
using WeLearn.Shared.Extensions.Paging;

namespace WeLearn.Api.Services.Notice;

public class NoticeService : INoticeService
{
    private readonly ApplicationDbContext _dbContext;

    public NoticeService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResponseDto<GetStudyYearNoticeDto>> GetStudyYearNoticesAsync(Guid studyYearId, PageOptionsDto pageOptions)
    {
        if (!await _dbContext.StudyYears
            .AsNoTracking()
            .AnyAsync(sy => sy.Id == studyYearId))
            throw new StudyYearNotFoundException();

        var dtos = await _dbContext.StudyYearNotices
            .AsNoTracking()
            .Include(syn => syn.Creator)
            .Include(syn => syn.Documents)
                .ThenInclude(d => d.Creator)
            .Include(syn => syn.Comments)
            .Where(syn => syn.StudyYearId == studyYearId)
            .Select(syn => syn.WithDocumentCount())
            .Select(syn => syn.WithCommentCount())
            .GetPagedResponseDtoAsync(pageOptions, MapStudyYearNoticeToGetDto());

        return dtos;
    }

    private static Expression<Func<StudyYearNotice, GetStudyYearNoticeDto>> MapStudyYearNoticeToGetDto()
    {
        return syn => syn.MapToGetDto();
    }
}
