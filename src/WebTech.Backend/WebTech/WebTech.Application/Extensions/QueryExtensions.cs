using Microsoft.EntityFrameworkCore;

namespace WebTech.Application.Extensions;

public static class QueryExtensions
{
    public static IQueryable<TEntity> ApplyTracking<TEntity>(this IQueryable<TEntity> query, bool isTracking)
        where TEntity : class => isTracking ? query.AsTracking() : query.AsNoTracking();
}