using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
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

    public async Task<TResult> FindByConditionAsync<TResult>(Expression<Func<TEntity, bool>> condition, Func<IQueryable<TEntity>, Task<TResult>> queryFunc, bool isTracking = false)
    {
        var query = isTracking
            ? _dbContext.Set<TEntity>().AsTracking() 
            : _dbContext.Set<TEntity>().AsNoTracking();
        
        
        return await queryFunc(query.Where(condition));
    }
}