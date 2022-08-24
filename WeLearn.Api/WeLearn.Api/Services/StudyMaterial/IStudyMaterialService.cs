using WeLearn.Api.Dtos.StudyMaterial;
using WeLearn.Shared.Dtos.Paging;

namespace WeLearn.Api.Services.StudyMaterial
{
    public interface IStudyMaterialService
    {
        Task<PagedResponseDto<GetStudyMaterialDto>> GetCourseStudyMaterialsAsync(Guid courseId, PageOptionsDto pageOptions);
    }
}