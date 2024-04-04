using System.Linq.Expressions;

namespace Core.Application.Abstractions.Persistence.Repository.Read;

public interface IAsyncRead<TEntity>
{
    Task<List<TEntity>> ToListAsync(CancellationToken cancellationToken);
    Task<List<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    Task<List<TResult>> ToListAsync<TResult>(IQueryable<TResult> queryable, CancellationToken cancellationToken);
    
    Task<TEntity[]> ToArrayAsync(CancellationToken cancellationToken);
    Task<TEntity[]> ToArrayAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    Task<TResult[]> ToArrayAsync<TResult>(IQueryable<TResult> queryable, CancellationToken cancellationToken);
    
    Task<int> CountAsync(CancellationToken cancellationToken);
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    Task<int> CountAsync<TResult>(IQueryable<TResult> queryable, CancellationToken cancellationToken);
    
    Task<bool> AnyAsync(CancellationToken cancellationToken);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    Task<bool> AnyAsync<TResult>(IQueryable<TResult> queryable, CancellationToken cancellationToken);

    Task<TEntity> SingleAsync(CancellationToken cancellationToken);
    Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    Task<TResult> SingleAsync<TResult>(IQueryable<TResult> queryable, CancellationToken cancellationToken);
    
    Task<TEntity?> SingleOrDefaultAsync(CancellationToken cancellationToken);
    Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    Task<TResult?> SingleOrDefaultAsync<TResult>(IQueryable<TResult> queryable, CancellationToken cancellationToken);

    Task<TEntity?> FirstOrDefaultAsync(CancellationToken cancellationToken);
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    Task<TResult?> FirstOrDefaultAsync<TResult>(IQueryable<TResult> queryable, CancellationToken cancellationToken);
}