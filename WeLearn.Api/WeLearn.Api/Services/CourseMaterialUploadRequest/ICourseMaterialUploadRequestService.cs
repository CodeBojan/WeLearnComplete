using WeLearn.Api.Dtos.CourseMaterialUploadRequest;
using WeLearn.Shared.Dtos.Paging;

namespace WeLearn.Api.Services.CourseMaterialUploadRequest
{
    public interface ICourseMaterialUploadRequestService
    {
        Task ApproveCourseMaterialUploadRequestAsync(Guid courseMaterialUploadRequestId);
        Task<GetCourseMaterialUploadRequestDto> CreateCourseMaterialUploadRequestDtoAsync(Guid courseId, PostCourseMaterialUploadRequestDto postDto, IEnumerable<IFormFile> formFiles, Guid creatorId);
        Task<GetCourseMaterialUploadRequestDto> GetCourseMaterialUploadRequestAsync(Guid courseMaterialUploadRequestId);
        Task<PagedResponseDto<GetCourseMaterialUploadRequestDto>> GetCourseMaterialUploadRequestsAsync(Guid courseId, PageOptionsDto pageOptions);
    }
}