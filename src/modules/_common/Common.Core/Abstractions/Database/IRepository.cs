using System.Linq.Expressions;
using Common.Core.Abstractions.Entities;

namespace Common.Core.Abstractions.Database;

/// <summary>
///     Represents a repository interface that combines query and command operations for a specific entity type.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <typeparam name="TKey">The type of the entity identifier.</typeparam>
public interface IRepository<TEntity, in TKey> : IQueryRepository<TEntity, TKey>,
                                                 ICommandRepository<TEntity, TKey>
    where TEntity : IEntity<TKey> { }

/// <summary>
///     Represents a repository interface for querying entities.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <typeparam name="TKey">The type of the entity identifier.</typeparam>
public interface IQueryRepository<TEntity, in TKey> where TEntity : IEntity<TKey>
{
    /// <summary>
    ///     Finds an entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    Task<TEntity?> FindByIdAsync(TKey id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Finds a single entity that matches the specified predicate.
    /// </summary>
    /// <param name="predicate">The predicate to filter entities.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate,
                                CancellationToken cancellationToken = default);

    /// <summary>
    ///     Finds entities that match the specified predicate.
    /// </summary>
    /// <param name="predicate">The predicate to filter entities.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A read-only list of entities that match the predicate.</returns>
    Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate,
                                           CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets all entities.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A read-only list of all entities.</returns>
    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Checks if any entity matches the specified predicate.
    /// </summary>
    /// <param name="predicate">The predicate to filter entities.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>True if any entity matches the predicate; otherwise, false.</returns>
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
}

/// <summary>
///     Represents a repository interface for performing command operations on entities.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <typeparam name="TKey">The type of the entity identifier.</typeparam>
public interface ICommandRepository<TEntity, in TKey> where TEntity : IEntity<TKey>
{
    /// <summary>
    ///     Adds a new entity.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Updates an existing entity.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Deletes a range of entities.
    /// </summary>
    /// <param name="entities">The entities to delete.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    Task DeleteRangeAsync(IReadOnlyList<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Deletes entities that match the specified predicate.
    /// </summary>
    /// <param name="predicate">The predicate to filter entities to delete.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Deletes a specific entity.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Deletes an entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity to delete.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    Task DeleteByIdAsync(TKey id, CancellationToken cancellationToken = default);
}