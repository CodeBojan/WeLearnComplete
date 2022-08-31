using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using WeLearn.Api.Dtos.CourseMaterialUploadRequest;
using WeLearn.Api.Exceptions.Models;
using WeLearn.Api.Extensions.Models;
using WeLearn.Data.Models.Content;
using WeLearn.Data.Persistence;
using WeLearn.Importers.Services.File;
using WeLearn.Shared.Dtos.Paging;
using WeLearn.Shared.Exceptions.Models;
using WeLearn.Shared.Extensions.Paging;

namespace WeLearn.Api.Services.CourseMaterialUploadRequest;

public class CourseMaterialUploadRequestService : ICourseMaterialUploadRequestService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IFilePersistenceService _filePersistenceService;

    public CourseMaterialUploadRequestService(ApplicationDbContext dbContext, IFilePersistenceService filePersistenceService)
    {
        _dbContext = dbContext;
        _filePersistenceService = filePersistenceService;
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

    public async Task<PagedResponseDto<GetCourseMaterialUploadRequestDto>> GetApprovedCourseMaterialUploadRequestsAsync(
        Guid courseId,
        PageOptionsDto pageOptions)
    {
        var course = await _dbContext.Courses.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == courseId);
        if (course is null)
            throw new CourseNotFoundException();

        var dtos = await GetCourseMaterialsNoTracking()
            .Where(cmur => cmur.CourseId == courseId && cmur.IsApproved)
            .GetPagedResponseDtoAsync(pageOptions, MapCourseMaterialUploadRequestToDto());

        return dtos;
    }

    public async Task<PagedResponseDto<GetCourseMaterialUploadRequestDto>> GetUnapprovedCourseMaterialUploadRequestsAsync(
        Guid courseId, PageOptionsDto pageOptions)
    {
        var course = await _dbContext.Courses.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == courseId);
        if (course is null)
            throw new CourseNotFoundException();
            
        var dtos = await GetCourseMaterialsNoTracking()
            .Where(cmur => cmur.CourseId == courseId && !cmur.IsApproved)
            .GetPagedResponseDtoAsync(pageOptions, MapCourseMaterialUploadRequestToDto());

        return dtos;
    }

    public async Task<GetCourseMaterialUploadRequestDto> CreateCourseMaterialUploadRequestDtoAsync(Guid courseId, PostCourseMaterialUploadRequestDto postDto, IEnumerable<IFormFile> formFiles, Guid creatorId)
    {
        if (!postDto?.Documents?.Any() ?? false)
            throw new CourseMaterialUploadRequestValidationException("No documents specified");

        if (!formFiles?.Any() ?? false)
            throw new CourseMaterialUploadRequestValidationException("No form files specified");

        if (postDto.Documents.Length != formFiles.Count())
            throw new CourseMaterialUploadRequestValidationException();

        var course = await _dbContext.Courses.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == courseId);
        if (course is null)
            throw new CourseNotFoundException();

        var cmur = new WeLearn.Data.Models.CourseMaterialUploadRequest(postDto.Title, postDto.Body, false, postDto.Remark, null, creatorId, courseId);

        var documents = new List<WeLearn.Data.Models.Content.Document>();
        for (int i = 0; i < postDto.Documents.Length; i++)
        {
            var documentDto = postDto.Documents[i];
            var formFile = formFiles.ElementAt(i);

            using var webStream = formFile.OpenReadStream();

            var documentUri = await _filePersistenceService.DownloadFileAsync(webStream, "userDocuments", CancellationToken.None);

            string hash;
            string hashAlgo;

            (hash, hashAlgo) = await _filePersistenceService.GetFileHashAsync(documentUri, CancellationToken.None);

            var fileName = Path.GetFileName(formFile.FileName);
            var fileExtension = Path.GetExtension(formFile.FileName);
            var fileSize = new FileInfo(documentUri).Length;

            var document = new WeLearn.Data.Models.Content.Document(null, null, documentDto.Body, documentDto.Title, documentDto.Author, false, courseId, creatorId, null, null, fileName, documentUri, documentDto.Version, fileSize, hash, hashAlgo, null, cmur.Id, fileExtension);

            documents.Add(document);
        }

        _dbContext.Add(cmur);
        _dbContext.AddRange(documents);
        await _dbContext.SaveChangesAsync();

        return cmur.MapToGetDto();
    }

    public async Task ApproveCourseMaterialUploadRequestAsync(Guid courseMaterialUploadRequestId)
    {
        var uploadRequest = await _dbContext.CourseMaterialUploadRequests
                  .Include(cmur => cmur.Documents)
                  .FirstOrDefaultAsync(cmur => cmur.Id == courseMaterialUploadRequestId);
        if (uploadRequest is null)
            throw new CourseMaterialUploadRequestNotFoundException();

        if (uploadRequest.IsApproved)
            throw new CourseMaterialUploadRequestAlreadyApprovedException();

        uploadRequest.IsApproved = true;
        var studyMaterial = new WeLearn.Data.Models.Content.StudyMaterial(null, null, uploadRequest.Title, uploadRequest.Body, null, false, uploadRequest.CourseId, uploadRequest.CreatorId, null, null);
        foreach (var document in uploadRequest.Documents)
        {
            document.DocumentContainerId = studyMaterial.Id;
        }

        _dbContext.Add(studyMaterial);

        await _dbContext.SaveChangesAsync();
    }

    // TODO for updating

    private IQueryable<WeLearn.Data.Models.CourseMaterialUploadRequest> GetCourseMaterialsNoTracking()
    {
        return _dbContext.CourseMaterialUploadRequests
                    .AsNoTracking()
                    .Include(cmur => cmur.Documents)
                    .Include(cmur => cmur.Creator)
                        .ThenInclude(a => a.User)
                    .OrderByDescending(cmur => cmur.UpdatedDate);
    }

    private static Expression<Func<WeLearn.Data.Models.CourseMaterialUploadRequest, GetCourseMaterialUploadRequestDto>> MapCourseMaterialUploadRequestToDto()
    {
        return cmur => cmur.MapToGetDto();
    }
}
