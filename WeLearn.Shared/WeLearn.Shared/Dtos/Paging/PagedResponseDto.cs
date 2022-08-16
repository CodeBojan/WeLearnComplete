using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Shared.Dtos.Paging;

public class PagedResponseDto<T>
{
    public int Limit { get; set; }
    public int Page { get; set; }
    public int? TotalPages { get; set; }
    public IEnumerable<T> Data { get; set; } = new List<T>();

    public void ApplyPagingOptions(PageOptionsDto pagingOptions)
    {
        Limit = pagingOptions.Limit;
        Page = pagingOptions.Page;
    }
}
