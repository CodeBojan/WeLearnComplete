using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using WeLearn.Api.Dtos.CourseMaterialUploadRequest;
using WeLearn.Api.Exceptions.Models;
using WeLearn.Api.Extensions.Models;
using WeLearn.Data.Models.Content;
using WeLearn.Data.Persistence;
using WeLearn.Shared.Dtos.Paging;
using WeLearn.Shared.Extensions.Paging;

namespace WeLearn.Api.Services.CourseMaterialUploadRequest;

public class CourseMaterialUploadRequestService : ICourseMaterialUploadRequestService
{
    private readonly ApplicationDbContext _dbContext;

    public CourseMaterialUploadRequestService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetCourseMaterialUploadRequestDto> GetCourseMaterialUploadRequestAsync(Guid courseMaterialUploadRequestId)
    {
        var cmur = await GetCourseMaterialsNoTracking()
                .Where(cmur => cmur.Id == courseMaterialUploadRequestId)
                .Select(MapCourseMaterialUploadRequestToDto())
                .FirstOrDefaultAsync();

        if (cmur is null)
            throw new CourseMaterialUploadRequestNotFoundException();

        return cmur;
    }

    public async Task<PagedResponseDto<GetCourseMaterialUploadRequestDto>> GetCourseMaterialUploadRequestsAsync(
        Guid courseId,
        PageOptionsDto pageOptions)
    {
        var course = await _dbContext.Courses.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == courseId);
        if (course is null)
            throw new CourseNotFoundException();

        var dtos = await GetCourseMaterialsNoTracking()
            .Where(cmur => cmur.CourseId == courseId)
            .GetPagedResponseDtoAsync(pageOptions, MapCourseMaterialUploadRequestToDto());

        return dtos;
    }

    // TODO for creating
    //public async Task<PagedResponseDto<GetCourseMaterialUploadRequestDto>> CreateCourseMaterialUploadRequestAsync(Guid courseId, )

    // TODO for approving - authorize only the admin for the course
    // TODO for updating

    private IIncludableQueryable<WeLearn.Data.Models.CourseMaterialUploadRequest, ICollection<Document>> GetCourseMaterialsNoTracking()
    {
        return _dbContext.CourseMaterialUploadRequests
                    .AsNoTracking()
                    .Include(cmur => cmur.Documents);
    }

    private static Expression<Func<WeLearn.Data.Models.CourseMaterialUploadRequest, GetCourseMaterialUploadRequestDto>> MapCourseMaterialUploadRequestToDto()
    {
        return cmur => cmur.MapToGetDto();
    }
}
