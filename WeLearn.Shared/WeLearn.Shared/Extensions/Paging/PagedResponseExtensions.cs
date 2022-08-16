using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Shared.Dtos.Paging;
using WeLearn.Shared.Exceptions;

namespace WeLearn.Shared.Extensions.Paging;

public static class PagedResponseExtensions
{
    public const int MaxInclusiveGlobalPageSize = 10;

    public static void SetTotalPages(long totalItems, PagedResponseDto<object> pagedResponseDto)
    {
        pagedResponseDto.TotalPages = (int)GetTotalPages(totalItems, pagedResponseDto.Limit);
    }

    public static long GetTotalPages(long totalItems, double limit)
    {
        var actualPages = (int)Math.Ceiling(totalItems / limit);
        return actualPages > 0 ? actualPages : 1;
    }

    public static long GetTotalPages(long totalItems, PageOptionsDto pagingOptionsDto)
    {
        return GetTotalPages(totalItems, pagingOptionsDto.Limit);
    }

    // TODO replace exception throwing with try_ approach
    public static async Task<long> CountTotalPagesAsync<T>(this IQueryable<T> queryable, PageOptionsDto pagingOptions)
    {
        var totalItems = await queryable.LongCountAsync();
        var totalPages = GetTotalPages(totalItems, pagingOptions);
        if (pagingOptions.Page == 0 || pagingOptions.Page > totalPages)
            throw new PageOutOfBoundsException(pagingOptions.Page);
        return totalPages;
    }

    public static async Task<PagedResponseDto<TResponse>> GetPagedResponseDtoAsync<TQuery, TResponse>(this IQueryable<TQuery> queryable, PageOptionsDto pageOptionsDto, Expression<Func<TQuery, TResponse>> selectExpression, bool countTotalPages = true)
    {
        if (pageOptionsDto.Limit > MaxInclusiveGlobalPageSize)
            pageOptionsDto.Limit = MaxInclusiveGlobalPageSize;

        long? totalPages = countTotalPages ? await queryable.CountTotalPagesAsync(pageOptionsDto) : null;

        var tResponses = await queryable
                .Paginate(pageOptionsDto)
                .Select(selectExpression)
                .ToListAsync();

        var pagedResponseDto = new PagedResponseDto<TResponse>
        {
            Data = tResponses
        };

        pagedResponseDto.TotalPages = (int?)totalPages;
        pagedResponseDto.ApplyPagingOptions(pageOptionsDto);

        return pagedResponseDto;
    }
}
