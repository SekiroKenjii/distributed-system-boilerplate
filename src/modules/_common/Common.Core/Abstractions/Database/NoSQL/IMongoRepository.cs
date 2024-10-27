using System.Linq.Expressions;
using Common.Core.Abstractions.Entities.NoSQL;
using MongoDB.Bson;
using MongoDB.Driver.Linq;

namespace Common.Core.Abstractions.Database.NoSQL;

/// <summary>
///     Represents a repository for managing NoSQL documents in MongoDB.
/// </summary>
/// <typeparam name="TDocument">The type of the document.</typeparam>
public interface IMongoRepository<TDocument> : IRepository<TDocument, ObjectId> where TDocument : IDocument
{
    /// <summary>
    ///     Returns an <see cref="IMongoQueryable{TDocument}" /> for querying documents.
    /// </summary>
    /// <param name="predicate">An optional filter expression.</param>
    /// <returns>An <see cref="IMongoQueryable{TDocument}" /> for querying documents.</returns>
    IMongoQueryable<TDocument> AsQueryable(Expression<Func<TDocument, bool>>? predicate = null);
}