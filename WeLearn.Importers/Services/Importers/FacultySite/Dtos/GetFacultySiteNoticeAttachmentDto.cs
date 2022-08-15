using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Importers.Services.Importers.FacultySite.Dtos;

public class GetFacultySiteNoticeAttachmentDto
{
    public string PreviewName { get; set; }
    public long FileSize { get; set; }
    public string Url { get; set; }
    public DateTime CreatedDate { get; set; }
}
