using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models.Content;

public class ContentBase : BaseEntity
{
    public long? ExternalId { get; set; }
    public string? ExternalUrl { get; set; }
    public string Body { get; set; }
    public string Title { get; set; }
    public string? Author { get; set; }
    public bool IsImported { get; set; }
    public Guid CourseId { get; set; }
    public ContentType ContentType { get; set; }

    public virtual Course Course { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
}
