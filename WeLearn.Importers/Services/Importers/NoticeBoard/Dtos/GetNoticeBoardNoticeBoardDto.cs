using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeLearn.Importers.Services.Importers.NoticeBoard.Dtos;

public class GetNoticeBoardNoticeBoardDto
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("naziv")] public NoticeBoardBoardName BoardName { get; set; }
    [JsonPropertyName("opis")] public string Description { get; set; }
    [JsonPropertyName("napomena")] public string Remark { get; set; }
}
