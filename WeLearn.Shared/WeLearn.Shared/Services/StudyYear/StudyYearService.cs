using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Persistence;
using WeLearn.Shared.Dtos.Paging;
using WeLearn.Shared.Dtos.StudyYear;
using WeLearn.Shared.Exceptions.Models;
using WeLearn.Shared.Extensions.Models;
using WeLearn.Shared.Extensions.Paging;

namespace WeLearn.Shared.Services.StudyYear;

public class StudyYearService : IStudyYearService
{
    private readonly ApplicationDbContext _dbContext;

    public StudyYearService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetStudyYearDto> GetStudyYearAsync(Guid studyYearId)
    {
        var dto = await _dbContext.StudyYears
            .Where(sy => sy.Id == studyYearId)
            .Select(MapStudyYearToGetDto())
            .FirstOrDefaultAsync();

        if (dto is null)
            throw new StudyYearNotFoundException();

        return dto;
    }

    public async Task<GetStudyYearDto> CreateStudyYearAsync(string shortName, string fullName, string description)
    {
        // TODO unique index
        var existingStudyYear = await _dbContext.StudyYears.FirstOrDefaultAsync(sy => sy.ShortName == shortName || sy.FullName == fullName);
        if (existingStudyYear is not null)
            throw new StudyYearAlreadyExistsException();

        existingStudyYear = new Data.Models.StudyYear(shortName, fullName, description);
        _dbContext.Add(existingStudyYear);

        await _dbContext.SaveChangesAsync();

        return existingStudyYear.MapToGetDto();
    }

    public async Task<PagedResponseDto<GetStudyYearDto>> GetStudyYearsAsync(PageOptionsDto pageOptions)
    {
        var dto = await _dbContext.StudyYears
            .AsNoTracking()
            .OrderByDescending(sy => sy.UpdatedDate)
            .GetPagedResponseDtoAsync(pageOptions, MapStudyYearToGetDto());

        return dto;
    }

    private static Expression<Func<Data.Models.StudyYear, GetStudyYearDto>> MapStudyYearToGetDto()
    {
        return sy => sy.MapToGetDto();
    }
}
