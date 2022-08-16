using Microsoft.EntityFrameworkCore;
using WeLearn.Api.Dtos.FollowedStudyYear;
using WeLearn.Data.Persistence;

namespace WeLearn.Api.Services.FollowedStudyYear;

public class FollowedStudyYearService : IFollowedStudyYearService
{
    private readonly ApplicationDbContext dbContext;

    public FollowedStudyYearService(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<GetFollowedStudyYearDto> FollowStudyYearAsync(Guid accountId, Guid studyYearId)
    {
        var existingFollowedStudyYear = await dbContext.FollowedStudyYears
            .AsNoTracking()
            .FirstOrDefaultAsync(fc => fc.AccountId == accountId && fc.StudyYearId == studyYearId);

        if (existingFollowedStudyYear is not null)
            return new GetFollowedStudyYearDto
            {
                AccountId = accountId,
                StudyYearId = studyYearId
            };

        var followedStudyYear = new WeLearn.Data.Models.FollowedStudyYear(accountId, studyYearId);

        dbContext.Add(followedStudyYear);
        await dbContext.SaveChangesAsync();

        return new GetFollowedStudyYearDto
        {
            AccountId = accountId,
            StudyYearId = studyYearId
        };
    }
}
