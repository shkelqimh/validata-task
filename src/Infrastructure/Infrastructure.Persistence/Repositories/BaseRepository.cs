using System.Linq.Expressions;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Shared.ExtensionMethods;

namespace Infrastructure.Persistence.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet
            .Where(predicate)
            .Paginate(pageNumber, pageSize)
            .AsQueryable();

        if (includes is not null)
        {
            query = includes.Aggregate(query, 
                (current, include) 
                    => current.Include(include));
        }
        
        return await query.ToListAsync();
    }

    public virtual async Task<List<TEntity>> GetAllAsync(int pageNumber, int pageSize, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet
            .Paginate(pageNumber, pageSize)
            .AsQueryable();

        if (includes is not null)
        {
            query = includes.Aggregate(query, 
                (current, include) 
                    => current.Include(include));
        }
        
        return await query.ToListAsync();
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet;

        if (includes is not null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.FirstOrDefaultAsync(e => e.Id == id);
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await Task.CompletedTask;
    }

    public virtual Task DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }
    
    public Task<int> CountAsync()
    {
        return _dbSet.CountAsync();
    }

    public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.CountAsync(predicate);
    }
}
