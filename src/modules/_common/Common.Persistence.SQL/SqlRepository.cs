using System.Linq.Expressions;
using Common.Core.Abstractions.Database.SQL;
using Common.Core.Abstractions.Entities.SQL;
using Common.Core.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Common.Persistence.SQL;

public class SqlRepository<TCtx, TEntity, TKey>(TCtx dbContext)
    : RelationalEntityQueryCompiler<TCtx, TEntity, TKey>, ISqlRepository<TCtx, TEntity, TKey>
    where TCtx : DbContext
    where TEntity : EntityBase<TKey>
{
    private readonly TCtx _dbContext = ISqlRepository<TCtx, TEntity, TKey>.ValidateDbContext(dbContext);

    public async Task<TEntity?> FindByIdAsync(TKey id, CancellationToken cancellationToken = default)
    {
        return await FirstOrDefaultByIdAsync(_dbContext, id);
    }

    public async Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate,
                                             CancellationToken cancellationToken = default)
    {
        return await FirstOrDefaultAsync(_dbContext, predicate);
    }

    public async Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate,
                                                        CancellationToken cancellationToken = default)
    {
        return await _dbContext.BuildRelationalEntityQuery<TCtx, TEntity, TKey>()
                               .Where(predicate)
                               .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.BuildRelationalEntityQuery<TCtx, TEntity, TKey>().ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate,
                                        CancellationToken cancellationToken = default)
    {
        return await _dbContext.BuildRelationalEntityQuery<TCtx, TEntity, TKey>(false, false)
                               .AnyAsync(predicate, cancellationToken);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.AddAsync(entity, cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(() => _dbContext.Update(entity), cancellationToken);
    }

    public async Task DeleteRangeAsync(IReadOnlyList<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await Task.Run(() => _dbContext.RemoveRange(entities), cancellationToken);
    }

    public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate,
                                  CancellationToken cancellationToken = default)
    {
        var entity = await FirstOrDefaultAsync(_dbContext, predicate);

        if (entity is null) return;

        await Task.Run(() => _dbContext.Remove(entity), cancellationToken);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(() => _dbContext.Remove(entity), cancellationToken);
    }

    public async Task DeleteByIdAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var entity = await FirstOrDefaultByIdAsync(_dbContext, id);

        if (entity is null) return;

        await Task.Run(() => _dbContext.Remove(entity), cancellationToken);
    }
}