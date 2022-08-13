using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models.Content;

public class CourseMaterial : Content
{
    public CourseMaterial(
        long? externalId,
        string? externalUrl,
        string body,
        string title,
        string? author,
        bool isImported,
        Guid? courseId,
        string type,
        Guid? creatorId,
        Guid? externalSystemId) : base(
            externalId,
            externalUrl,
            body,
            title,
            author,
            isImported,
            courseId,
            type,
            creatorId,
            externalSystemId)
    {
    }
}
