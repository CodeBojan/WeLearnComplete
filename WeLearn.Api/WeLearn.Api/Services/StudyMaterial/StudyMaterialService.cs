﻿using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using WeLearn.Api.Dtos.StudyMaterial;
using WeLearn.Api.Exceptions.Models;
using WeLearn.Api.Extensions.Models;
using WeLearn.Data.Models.Content;
using WeLearn.Data.Persistence;
using WeLearn.Shared.Dtos.Paging;
using WeLearn.Shared.Exceptions.Models;
using WeLearn.Shared.Extensions.Paging;

namespace WeLearn.Api.Services.StudyMaterial;

public class StudyMaterialService : IStudyMaterialService
{
    private readonly ApplicationDbContext _dbContext;

    public StudyMaterialService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResponseDto<GetStudyMaterialDto>> GetCourseStudyMaterialsAsync(Guid courseId, PageOptionsDto pageOptions)
    {
        var course = await _dbContext.Courses
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == courseId);
        if (course is null)
            throw new CourseNotFoundException();

        var dtos = await _dbContext.StudyMaterials
            .AsNoTracking()
            .Include(sm => sm.Documents)
            .Include(sm => sm.Creator)
                .ThenInclude(a => a.User)
            .Include(sm => sm.Comments)
            .Where(sm => sm.CourseId == courseId)
            .OrderByDescending(sm => sm.UpdatedDate)
            .Select(sm => sm.WithDocumentCount())
            .Select(sm => sm.WithCommentCount())
            .Cast<WeLearn.Data.Models.Content.StudyMaterial>()
            .GetPagedResponseDtoAsync(pageOptions, MapStudyMaterialToDto());

        return dtos;
    }

    private static Expression<Func<WeLearn.Data.Models.Content.StudyMaterial, GetStudyMaterialDto>> MapStudyMaterialToDto()
    {
        return sm => sm.MapToGetDto();
    }
}
