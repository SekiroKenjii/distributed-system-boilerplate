using Common.Core.Abstractions.Entities.SQL;
using Microsoft.EntityFrameworkCore;

namespace Common.Core.Abstractions.Database.SQL;

/// <summary>
///     Represents a repository for managing SQL entities in a database context.
/// </summary>
/// <typeparam name="TCtx">The type of the database context.</typeparam>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <typeparam name="TKey">The type of the entity key.</typeparam>
public interface ISqlRepository<TCtx, TEntity, in TKey> : IRepository<TEntity, TKey>
    where TCtx : DbContext
    where TEntity : EntityBase<TKey>
{
    /// <summary>
    ///     Validates the provided database context.
    /// </summary>
    /// <param name="dbContext">The database context to validate.</param>
    /// <returns>The validated database context.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the database context is null.</exception>
    protected static TCtx ValidateDbContext(TCtx dbContext)
    {
        if (dbContext is null)
            throw new InvalidOperationException($"The SQL database context \"{typeof(TCtx).Name}\" is not set.");

        return dbContext;
    }
}