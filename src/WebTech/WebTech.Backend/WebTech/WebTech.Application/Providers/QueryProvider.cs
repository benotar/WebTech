using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebTech.Application.Extensions;
using WebTech.Application.Interfaces.Persistence;
using WebTech.Application.Interfaces.Providers;

namespace WebTech.Application.Providers;

public class QueryProvider<TEntity> : IQueryProvider<TEntity> where TEntity : class
{
    private readonly IWebTechDbContext _dbContext;

    public QueryProvider(IWebTechDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Expression<Func<TEntity, bool>> ByPropertyName(string propertyName, string propertyValue,
        bool isEntityNameBeginLower = false)
    {
        if (isEntityNameBeginLower)
        {
            propertyName = propertyName.ToValidPropertyName();
        }

        return entity => EF.Property<string>(entity,
            propertyName).Equals(propertyValue);
    }

    public Expression<Func<TEntity, bool>> ByAuthorFullName(string firstName, string lastName)
    {
        return entity => EF.Property<string>(entity, nameof(firstName).ToValidPropertyName()).Equals(firstName)
                         && EF.Property<string>(entity, nameof(lastName).ToValidPropertyName()).Equals(lastName);
    }

    public Expression<Func<TEntity, bool>> ByEntityId(string nameEntityId, Guid entityId,
        bool isEntityForeignKey = false)
    {
        nameEntityId = isEntityForeignKey
            ? nameEntityId.ToValidPropertyName()
            : nameEntityId.ToValidEntityIdPropertyName();
        
        return entity => EF.Property<Guid>(entity,
            nameEntityId).Equals(entityId);
    }

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