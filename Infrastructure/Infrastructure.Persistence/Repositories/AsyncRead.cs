using System.Linq.Expressions;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class AsyncRead<TEntity> : IAsyncRead<TEntity>
{
    private readonly IQueryable<TEntity> _internalQueryable;

    public AsyncRead(IQueryable<TEntity> internalQueryable)
    {
        _internalQueryable = internalQueryable;
    }

    public Task<List<TEntity>> ToListAsync(CancellationToken cancellationToken)
    {
        return _internalQueryable.ToListAsync(cancellationToken);
    }

    public Task<List<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return _internalQueryable.Where(predicate).ToListAsync(cancellationToken);
    }

    public Task<List<TResult>> ToListAsync<TResult>(IQueryable<TResult> queryable, CancellationToken cancellationToken)
    {
        return queryable.ToListAsync(cancellationToken);
    }

    public Task<TEntity[]> ToArrayAsync(CancellationToken cancellationToken)
    {
        return _internalQueryable.ToArrayAsync(cancellationToken);
    }

    public Task<TEntity[]> ToArrayAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        return _internalQueryable.Where(predicate).ToArrayAsync(cancellationToken);
    }

    public Task<TResult[]> ToArrayAsync<TResult>(IQueryable<TResult> queryable, CancellationToken cancellationToken)
    {
        return queryable.ToArrayAsync(cancellationToken);
    }

    public Task<int> CountAsync(CancellationToken cancellationToken)
    {
        return _internalQueryable.CountAsync(cancellationToken);
    }

    public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        return _internalQueryable.CountAsync(predicate, cancellationToken);
    }

    public Task<int> CountAsync<TResult>(IQueryable<TResult> queryable, CancellationToken cancellationToken)
    {
        return queryable.CountAsync(cancellationToken);
    }

    public Task<bool> AnyAsync(CancellationToken cancellationToken)
    {
        return _internalQueryable.AnyAsync(cancellationToken);
    }

    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        return _internalQueryable.AnyAsync(predicate, cancellationToken);
    }

    public Task<bool> AnyAsync<TResult>(IQueryable<TResult> queryable, CancellationToken cancellationToken)
    {
        return queryable.AnyAsync(cancellationToken);
    }

    public Task<TEntity?> SingleOrDefaultAsync(CancellationToken cancellationToken)
    {
        return _internalQueryable.SingleOrDefaultAsync(cancellationToken);
    }

    public Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return _internalQueryable.SingleOrDefaultAsync(predicate, cancellationToken);
    }

    public Task<TResult?> SingleOrDefaultAsync<TResult>(IQueryable<TResult> queryable,
        CancellationToken cancellationToken)
    {
        return queryable.SingleOrDefaultAsync(cancellationToken);
    }

    public Task<TEntity?> FirstOrDefaultAsync(CancellationToken cancellationToken)
    {
        return _internalQueryable.FirstOrDefaultAsync(cancellationToken);
    }

    public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return _internalQueryable.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public Task<TResult?> FirstOrDefaultAsync<TResult>(IQueryable<TResult> queryable,
        CancellationToken cancellationToken)
    {
        return queryable.FirstOrDefaultAsync(cancellationToken);
    }
    
    public Task<TEntity> SingleAsync(CancellationToken cancellationToken)
    {
        return _internalQueryable.SingleAsync(cancellationToken);
    }
    
    public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        return _internalQueryable.SingleAsync(predicate, cancellationToken);
    }
    
    public Task<TResult> SingleAsync<TResult>(IQueryable<TResult> queryable, CancellationToken cancellationToken)
    {
        return queryable.SingleAsync(cancellationToken);
    }
}