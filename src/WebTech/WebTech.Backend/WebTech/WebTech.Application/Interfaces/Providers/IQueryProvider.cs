using System.Linq.Expressions;

namespace WebTech.Application.Interfaces.Providers;

public interface IQueryProvider<TEntity> where TEntity : class
{
    Expression<Func<TEntity, bool>> ByAuthorFullName(string firstName, string lastName);
    Expression<Func<TEntity, bool>> ByEntityId(Guid entityId, bool isEntityForeignKey = false);

    Expression<Func<TEntity, bool>> ByPropertyName(string propertyName, string propertyValue, bool isEntityNameBeginLower = false);
    
    Task<TResult> ExecuteQueryAsync<TResult>(Func<IQueryable<TEntity>, Task<TResult>> queryFunc,
        Expression<Func<TEntity, bool>>? condition = null, bool isTracking = false);
}