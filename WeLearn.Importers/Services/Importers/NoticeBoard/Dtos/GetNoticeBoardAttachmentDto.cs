using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeLearn.Importers.Services.Importers.NoticeBoard.Dtos;

public class GetNoticeBoardAttachmentDto
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("naslov")] public string? Title { get; set; }
    [JsonPropertyName("velicina")] public long ByteSize { get; set; }
    [JsonPropertyName("originalniNaziv")] public string FileName { get; set; }
    [JsonPropertyName("ekstenzija")] public string FileExtension { get; set; }
}
