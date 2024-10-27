using System.Linq.Expressions;
using Common.Core.Abstractions.Entities.SQL;
using Microsoft.EntityFrameworkCore;

namespace Common.Core.Extensions;

/// <summary>
///     Provides extension methods for repository operations.
/// </summary>
public static class RepositoryExtensions
{
    /// <summary>
    ///     Builds a query for a relational entity with optional tracking and split query capabilities.
    /// </summary>
    /// <typeparam name="TCtx">The type of the database context.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TKey">The type of the entity key.</typeparam>
    /// <param name="dbContext">The database context.</param>
    /// <param name="isTracking">If set to <c>true</c>, the query will track changes.</param>
    /// <param name="isSplitQuery">If set to <c>true</c>, the query will be split.</param>
    /// <param name="includes">The related entities to include in the query.</param>
    /// <returns>An <see cref="IQueryable{TEntity}" /> representing the query.</returns>
    public static IQueryable<TEntity> BuildRelationalEntityQuery<TCtx, TEntity, TKey>(
        this TCtx dbContext,
        bool isTracking = true,
        bool isSplitQuery = true,
        params Expression<Func<TEntity, object?>>[]? includes) where TCtx : DbContext
                                                               where TEntity : EntityBase<TKey>
    {
        var entity = isTracking
                         ? dbContext.Set<TEntity>().AsTracking()
                         : dbContext.Set<TEntity>().AsNoTracking();

        if (includes == null || includes.Length == 0) return isSplitQuery ? entity.AsSplitQuery() : entity;

        Array.ForEach(includes, item => entity.Include(item));

        return isSplitQuery ? entity.AsSplitQuery() : entity;
    }
}