using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models;

public class CourseCredentials : DatedEntity
{
    public CourseCredentials(Guid credentialsId, Guid courseId)
    {
        CredentialsId = credentialsId;
        CourseId = courseId;
    }

    public Guid CourseId { get; set; }
    public Guid CredentialsId { get; set; }

    public virtual Course Course { get; set; }
    public virtual Credentials Credentials { get; set; }
}
