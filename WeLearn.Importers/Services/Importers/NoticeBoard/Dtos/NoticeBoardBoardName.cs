using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace WeLearn.Importers.Services.Importers.NoticeBoard.Dtos
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum NoticeBoardBoardName
    {
        [EnumMember(Value = "Прва година")]
        FirstYear,
        [EnumMember(Value = "Друга година")]
        SecondYear,
        [EnumMember(Value = "Трећа година")]
        ThirdYear,
        [EnumMember(Value = "Четврта година")]
        FourthYear
    }
}