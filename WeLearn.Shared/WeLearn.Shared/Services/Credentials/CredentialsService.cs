using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Models;
using WeLearn.Data.Persistence;
using WeLearn.Shared.Dtos.Credentials;
using WeLearn.Shared.Dtos.Paging;
using WeLearn.Shared.Exceptions.Models;
using WeLearn.Shared.Extensions.Models;
using WeLearn.Shared.Extensions.Paging;

namespace WeLearn.Shared.Services.Credentials;

public class CredentialsService : ICredentialsService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger _logger;

    public CredentialsService(ApplicationDbContext dbContext, ILogger<CredentialsService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<PagedResponseDto<GetCredentialsDto>> GetPagedCredentialsAsync(PageOptionsDto pageOptionsDto)
    {
        var queryable = _dbContext.Credentials.AsNoTracking();

        queryable = queryable
            .OrderByDescending(c => c.UpdatedDate);

        var responseDto = await queryable.GetPagedResponseDtoAsync(pageOptionsDto, MapCredentialsToGetDto());

        return responseDto;
    }

    public async Task<GetCredentialsDto?> GetCredentialsAsync(Guid credentialsId)
    {
        var credentials = await _dbContext.Credentials
            .AsNoTracking()
            .Where(c => c.Id == credentialsId)
            .Select(MapCredentialsToGetDto())
            .FirstOrDefaultAsync();

        return credentials;
    }

    public async Task<GetCredentialsDto> CreateCredentials(
        string username,
        string secret,
        Guid creatorId,
        Guid externalSystemId)
    {
        // TODO exception handling on duplicate credentials
        var credentials = new Data.Models.Credentials(username, secret, creatorId, externalSystemId);

        _dbContext.Add(credentials);
        await _dbContext.SaveChangesAsync();

        return credentials.MapToGetDto();
    }

    public async Task<GetCredentialsDto> AddCredentialsToCourseAsync(
        Guid courseId, Guid credentialsId)
    {
        var credentials = await _dbContext.Credentials
            .Where(c => c.Id == credentialsId)
            .FirstOrDefaultAsync();
        if (credentials is null)
             throw new CredentialsNotFoundException();

        var course = await _dbContext.Courses
            .Where(c => c.Id == courseId)
            .FirstOrDefaultAsync();
        if (course is null)
            throw new CourseNotFoundException();

        var courseCredentials = new CourseCredentials(credentialsId, courseId);

        credentials.AddCourseCredentials(courseCredentials);

        await _dbContext.SaveChangesAsync();

        return credentials.MapToGetDto();
    }

    private static Expression<Func<Data.Models.Credentials, GetCredentialsDto>> MapCredentialsToGetDto()
    {
        return c => c.MapToGetDto();
    }
}
