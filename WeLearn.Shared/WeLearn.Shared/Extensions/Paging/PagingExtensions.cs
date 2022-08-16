using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Shared.Dtos.Paging;

namespace WeLearn.Shared.Extensions.Paging;

public static class PagingExtensions
{
    private const int Count = 5;

    public static IQueryable<T> Paginate<T>(this IQueryable<T> query,
           int limit, int offset)
    {
        if (offset != 0)
            query = query.Skip(offset * limit);

        return limit == 0 ? query.Take(Count) : query.Take(limit);
    }

    public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable,
        PageOptionsDto options)
    {
        if (options is null) throw new NullReferenceException(nameof(options));

        return queryable
            .Skip(options.Offset)
            .Take(options.Limit > 0 ? options.Limit : Count);
    }

    public static IEnumerable<T> Paginate<T>(this IEnumerable<T> enumerable,
        PageOptionsDto options)
    {
        if (options is null) throw new NullReferenceException(nameof(options));

        return enumerable
            .Skip(options.Offset)
            .Take(options.Limit > 0 ? options.Limit : Count);
    }
}
