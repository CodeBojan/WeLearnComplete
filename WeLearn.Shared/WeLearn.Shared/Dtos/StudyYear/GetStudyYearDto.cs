using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Shared.Dtos.StudyYear;

public class GetStudyYearDto
{
    public Guid Id { get; internal set; }
    public DateTime CreatedDate { get; internal set; }
    public DateTime UpdatedDate { get; internal set; }
    public string ShortName { get; internal set; }
    public string FullName { get; set; }
    public string Description { get; set; }
    public int? FollowingCount { get; set; }
    public bool? IsFollowing { get; set; }
}
