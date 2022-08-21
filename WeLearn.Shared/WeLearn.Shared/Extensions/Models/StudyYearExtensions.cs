using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Models;
using WeLearn.Shared.Dtos.StudyYear;

namespace WeLearn.Shared.Extensions.Models;

public static class StudyYearExtensions
{
    public static GetStudyYearDto MapToGetDto(this StudyYear studyYear)
    {
        return new GetStudyYearDto
        {
            Id = studyYear.Id,
            CreatedDate = studyYear.CreatedDate,
            UpdatedDate = studyYear.UpdatedDate,
            ShortName = studyYear.ShortName,
            Description = studyYear.Description,
            FullName = studyYear.FullName,
            FollowingCount = studyYear.FollowingCount,
            IsFollowing = studyYear.IsFollowing
        };
    }
}
