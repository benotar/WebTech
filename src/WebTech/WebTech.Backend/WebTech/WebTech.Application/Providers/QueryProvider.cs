﻿using System.Collections;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebTech.Application.Extensions;
using WebTech.Application.Interfaces.Persistence;
using WebTech.Application.Interfaces.Providers;
using WebTech.Domain.Entities.Database;

namespace WebTech.Application.Providers;

public class QueryProvider<TEntity> : IQueryProvider<TEntity> where TEntity : class
{
    private readonly IWebTechDbContext _dbContext;

    public QueryProvider(IWebTechDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Expression<Func<TEntity, bool>> ByUserName(string userName)
        => entity => EF.Property<string>(entity, "UserName").Equals(userName);

    public async Task<TResult> ExecuteQueryAsync<TResult>(Func<IQueryable<TEntity>, Task<TResult>> queryFunc,
        Expression<Func<TEntity, bool>>? condition = null, bool isTracking = false)
    {
        var query = _dbContext.Set<TEntity>()
            .AsQueryable()
            .ApplyTracking(isTracking);

        if (condition is not null)
        {
            query = query.Where(condition);
        }

        return await queryFunc(query);
    }
}