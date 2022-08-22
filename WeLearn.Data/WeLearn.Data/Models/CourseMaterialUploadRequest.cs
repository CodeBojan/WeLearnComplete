using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Models.Content;

namespace WeLearn.Data.Models;

public class CourseMaterialUploadRequest : BaseEntity
{
    public CourseMaterialUploadRequest(
        string body,
        bool isApproved,
        string remark,
        string? type,
        Guid creatorId,
        Guid courseId)
    {
        Id = Guid.NewGuid();
        Body = body;
        IsApproved = isApproved;
        Remark = remark;
        Type = type;
        CreatorId = creatorId;
        CourseId = courseId;
    }

    public string Body { get; set; }
    public bool IsApproved { get; set; }
    public string Remark { get; set; }
    // TODO define somewhere - create,update
    public string? Type { get; set; }
    public Guid CreatorId { get; set; }
    public Guid CourseId { get; set; }

    public virtual Account Creator { get; set; }
    public virtual Course Course { get; set; }
    public virtual ICollection<Document> Documents { get; set; }
}
