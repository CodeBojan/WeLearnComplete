using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models.Content;

public class StudyMaterial : DocumentContainer
{
    public StudyMaterial(
        string? externalId,
        string? externalUrl,
        string body,
        string title,
        string? author,
        bool isImported,
        Guid? courseId,
        Guid? creatorId,
        Guid? externalSystemId,
        DateTime? externalCreatedDate) : base(
            externalId,
            externalUrl,
            body,
            title,
            author,
            isImported,
            courseId,
            ContentType.StudyMaterial.Value(),
            creatorId,
            externalSystemId,
            externalCreatedDate)
    {
        Id = Guid.NewGuid();
    }
}
