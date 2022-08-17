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
