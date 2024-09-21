using System.Linq.Expressions;

namespace WebTech.Application.Interfaces.Providers;

public interface IQueryProvider<TEntity> where TEntity : class
{
    Expression<Func<TEntity, bool>> ByUserName(string userName);
    
    Expression<Func<TEntity, bool>> ByUserId(Guid userId);
    
    Task<TResult> ExecuteQueryAsync<TResult>(Func<IQueryable<TEntity>, Task<TResult>> queryFunc,
        Expression<Func<TEntity, bool>>? condition = null, bool isTracking = false);
}