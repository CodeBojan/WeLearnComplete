using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeLearn.Importers.Services.Importers.NoticeBoard.Dtos;

public class GetNoticeBoardNoticeDto
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("naslov")] public string Title { get; set; }
    [JsonPropertyName("uvod")] public string Introduction { get; set; }
    [JsonPropertyName("sadrzaj")] public string Body { get; set; }
    [JsonPropertyName("potpis")] public string Author { get; set; }
    [JsonPropertyName("vrijemeKreiranja")] public DateTimeOffset CreatedDate { get; set; }
    [JsonPropertyName("vrijemeIsteka")] public DateTime ExpiryDate { get; set; }
    [JsonPropertyName("oglasnaPloca")] public GetNoticeBoardNoticeBoardDto NoticeBoard { get; set; }
    [JsonPropertyName("korisnickiNalog")][JsonIgnore] public string UserAccount { get; set; }
    [JsonPropertyName("oglasPrilozi")] public IEnumerable<GetNoticeBoardAttachmentDto> Attachments { get; set; }
}
