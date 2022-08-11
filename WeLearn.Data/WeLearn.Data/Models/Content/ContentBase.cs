using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models.Content;

// TODO abstract class
public class ContentBase
{
    public Guid Id { get; set; }
    public long ExternalId { get; set; }
}
