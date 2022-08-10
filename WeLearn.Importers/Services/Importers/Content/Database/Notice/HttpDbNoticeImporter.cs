using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Models.Content;

namespace WeLearn.Importers.Services.Importers.Content.Database.Notice;

public abstract class HttpDbNoticeImporter<TDto> : HttpDbImporter<Data.Models.Content.Notice, TDto>
    where TDto : class
{
}
