using WeLearn.Api.Dtos.CourseMaterialUploadRequest;
using WeLearn.Shared.Dtos.Paging;

namespace WeLearn.Api.Services.CourseMaterialUploadRequest
{
    public interface ICourseMaterialUploadRequestService
    {
        Task<GetCourseMaterialUploadRequestDto> GetCourseMaterialUploadRequestAsync(Guid courseMaterialUploadRequestId);
        Task<PagedResponseDto<GetCourseMaterialUploadRequestDto>> GetCourseMaterialUploadRequestsAsync(Guid courseId, PageOptionsDto pageOptions);
    }
}