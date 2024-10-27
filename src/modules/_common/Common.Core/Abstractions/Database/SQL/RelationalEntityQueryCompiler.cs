using System.Linq.Expressions;
using Common.Core.Abstractions.Entities.SQL;
using Common.Core.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Common.Core.Abstractions.Database.SQL;

/// <summary>
///     Provides abstract base class for compiling and caching commonly used asynchronous
///     entity queries in a relational database context.
/// </summary>
/// <typeparam name="TCtx">The type of the database context.</typeparam>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <typeparam name="TKey">The type of the entity key.</typeparam>
public abstract class RelationalEntityQueryCompiler<TCtx, TEntity, TKey> where TCtx : DbContext
                                                                         where TEntity : EntityBase<TKey>
{
    /// <summary>
    ///     Compiled asynchronous query to retrieve a single entity that matches
    ///     a specified predicate or returns <c>null</c> if no match is found.
    /// </summary>
    /// <remarks>
    ///     This query is optimized for single retrievals where a matching entity
    ///     may or may not exist.
    /// </remarks>
    protected static readonly Func<TCtx, Expression<Func<TEntity, bool>>, Task<TEntity?>>
        SingleOrDefaultAsync = EF.CompileAsyncQuery(
            (TCtx dbContext, Expression<Func<TEntity, bool>> pred) =>
                dbContext.BuildRelationalEntityQuery<TCtx, TEntity, TKey>().SingleOrDefault(pred)
        );

    /// <summary>
    ///     Compiled asynchronous query to retrieve the first entity that matches
    ///     a specified predicate or returns <c>null</c> if no match is found.
    /// </summary>
    /// <remarks>
    ///     This query is optimized for cases where multiple matches may exist but only
    ///     the first occurrence is required.
    /// </remarks>
    protected static readonly Func<TCtx, Expression<Func<TEntity, bool>>, Task<TEntity?>>
        FirstOrDefaultAsync = EF.CompileAsyncQuery(
            (TCtx dbContext, Expression<Func<TEntity, bool>> pred) =>
                dbContext.BuildRelationalEntityQuery<TCtx, TEntity, TKey>().FirstOrDefault(pred)
        );

    /// <summary>
    ///     Compiled asynchronous query to retrieve the first entity that matches a specified
    ///     identifier or returns <c>null</c> if no entity with that identifier is found.
    /// </summary>
    /// <remarks>
    ///     This query is optimized for fetching entities based on a unique identifier
    ///     and checks for non-null <c>Id</c> equality.
    /// </remarks>
    protected static readonly Func<TCtx, TKey, Task<TEntity?>> FirstOrDefaultByIdAsync =
        EF.CompileAsyncQuery(
            (TCtx dbContext, TKey id) =>
                dbContext.BuildRelationalEntityQuery<TCtx, TEntity, TKey>()
                         .FirstOrDefault(x => !Equals(x.Id, null) && x.Id.Equals(id))
        );
}