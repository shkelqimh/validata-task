using System.Linq.Expressions;
using Domain.Entities;

namespace Domain.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize, params Expression<Func<TEntity, object>>[] includes);
    Task<List<TEntity>> GetAllAsync(int pageNumber, int pageSize, params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity?> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] includes);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task<int> CountAsync();
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
}
