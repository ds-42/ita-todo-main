using Core.Application.Abstractions.Persistence.Repository.Read;
using Microsoft.EntityFrameworkCore;
using Core.Application.Abstractions.Persistence.Repository.Writing;

namespace Infrastructure.Persistence.Repositories;

public class BaseRepository<TEntity> : IBaseWriteRepository<TEntity> where TEntity : class
{
    private readonly DbContext _dbContext;

    protected readonly DbSet<TEntity> DbSet;

    public BaseRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        DbSet = _dbContext.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        var items = entities.ToArray();
        DbSet.AddRange(items);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return items;
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        DbSet.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        DbSet.Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        var items = entities.ToArray();
        DbSet.UpdateRange(items);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return items;
    }

    public async Task<int> RemoveAsync(TEntity entity, CancellationToken cancellationToken)
    {
        DbSet.Remove(entity);
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        DbSet.RemoveRange(entities);
        return await _dbContext.SaveChangesAsync(cancellationToken);

    }
    
    public IQueryable<TEntity> AsQueryable()
    {
        return DbSet.AsQueryable();
    }

    public IAsyncRead<TEntity> AsAsyncRead()
    {
        return new AsyncRead<TEntity>(DbSet.AsQueryable());
    }
}