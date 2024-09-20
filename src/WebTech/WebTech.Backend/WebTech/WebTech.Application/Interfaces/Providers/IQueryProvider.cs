using System.Linq.Expressions;

namespace WebTech.Application.Interfaces.Providers;

public interface IQueryProvider<TEntity> where TEntity: class
{
    Expression<Func<TEntity, bool>> ByUserName(string userName);

    Task<TResult> FindByConditionAsync<TResult>(
        Expression<Func<TEntity, bool>> condition,
        Func<IQueryable<TEntity>, Task<TResult>> queryFunc,
        bool isTracking = false);

    Task<List<TResult>> GetAsync<TResult>(Func<IQueryable<TEntity>, Task<List<TResult>>> queryFunc,
        Expression<Func<TEntity, bool>> condition = null, bool isTracking = false);
}