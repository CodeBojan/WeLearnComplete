using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models.Content;

public class Document : Content
{
    public string FileName { get; set; }
    public string Uri { get; set; }
    public long Size { get; set; }
    public string? Hash { get; set; }
    public string? HashAlgorithm { get; set; }
}
