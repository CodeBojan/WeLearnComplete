using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models.Content;

public class StudyMaterial : Content
{
    public int DocumentCount { get; set; }
    public ICollection<Document> Documents { get; set; }
}
