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

        var cmur = new WeLearn.Data.Models.CourseMaterialUploadRequest(postDto.Body, false, postDto.Remark, null, creatorId, courseId);

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

    // TODO for creating
    //public async Task<PagedResponseDto<GetCourseMaterialUploadRequestDto>> CreateCourseMaterialUploadRequestAsync(Guid courseId, )

    // TODO for approving - authorize only the admin for the course
    // TODO for updating

    private IIncludableQueryable<WeLearn.Data.Models.CourseMaterialUploadRequest, ICollection<WeLearn.Data.Models.Content.Document>> GetCourseMaterialsNoTracking()
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
