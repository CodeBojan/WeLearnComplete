using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models;

public class FollowedStudyYear : DatedEntity
{
    public FollowedStudyYear(Guid accountId, Guid studyYearId)
    {
        AccountId = accountId;
        StudyYearId = studyYearId;
    }

    public Guid AccountId { get; set; }
    public Guid StudyYearId { get; set; }

    public virtual Account Account { get; set; }
    public virtual StudyYear StudyYear { get; set; }
}
