using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Models.Content;

namespace WeLearn.Data.Models;

public class Comment : BaseEntity
{
    public string Body { get; set; }
    public Guid AuthorId { get; set; }
    public Guid ContentId { get; set; }

    public virtual Account Author { get; set; }
    public virtual Content.ContentBase Content { get; set; }
}
