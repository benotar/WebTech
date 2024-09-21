using System.Linq.Expressions;

namespace WebTech.Application.Interfaces.Providers;

public interface IQueryProvider<TEntity> where TEntity : class
{
    Expression<Func<TEntity, bool>> ByUserName(string userName);
    
    Expression<Func<TEntity, bool>> ByEntityId(Guid entityId);
    
    Task<TResult> ExecuteQueryAsync<TResult>(Func<IQueryable<TEntity>, Task<TResult>> queryFunc,
        Expression<Func<TEntity, bool>>? condition = null, bool isTracking = false);
}