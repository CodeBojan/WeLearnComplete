using WeLearn.Data.Models.Content;
using WeLearn.Importers.Services.Importers.Content;
using WeLearn.Importers.Services.Importers.Content.Database.Notice;
using WeLearn.Importers.Services.Importers.FacultySite.Dtos;

namespace WeLearn.Importers.Services.Importers.FacultySite.Content;

public interface IFacultySiteNoticeImporter : ITypedContentImporter<Notice, GetFacultySiteNoticeDto>, IFacultySiteImporter
{
}