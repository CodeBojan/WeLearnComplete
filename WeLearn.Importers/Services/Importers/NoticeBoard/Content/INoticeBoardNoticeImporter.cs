using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Models.Content;
using WeLearn.Importers.Services.Importers.Content;
using WeLearn.Importers.Services.Importers.Content.Database.Notice;
using WeLearn.Importers.Services.Importers.NoticeBoard.Dtos;

namespace WeLearn.Importers.Services.Importers.NoticeBoard.Content;

public interface INoticeBoardNoticeImporter : IDtoNoticeImporter<GetNoticeBoardNoticeDto>, INoticeBoardImporter
{
}
