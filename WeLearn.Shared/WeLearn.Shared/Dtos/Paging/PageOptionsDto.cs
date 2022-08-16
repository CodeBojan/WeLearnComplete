using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeLearn.Shared.Dtos.Paging;

public class PageOptionsDto
{
    private int limit = 1;
    private int page = 1;

    public int Limit
    {
        get => limit; set
        {
            limit = value > 1 ? value : 1;
        }
    }

    public int Page
    {
        get => page; set
        {
            page = value > 1 ? value : 1;
        }
    }

    [JsonIgnore]
    public int Offset => (Page - 1) * Limit;
}
