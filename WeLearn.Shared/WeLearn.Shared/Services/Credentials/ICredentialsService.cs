using WeLearn.Shared.Dtos.Credentials;
using WeLearn.Shared.Dtos.Paging;

namespace WeLearn.Shared.Services.Credentials
{
    public interface ICredentialsService
    {
        Task<GetCredentialsDto> AddCredentialsToCourseAsync(Guid courseId, Guid credentialsId);
        Task<GetCredentialsDto> CreateCredentials(string username, string secret, Guid creatorId, Guid externalSystemId);
        Task<GetCredentialsDto?> GetCredentialsAsync(Guid credentialsId);
        Task<PagedResponseDto<GetCredentialsDto>> GetPagedCredentialsAsync(PageOptionsDto pageOptionsDto);
    }
}