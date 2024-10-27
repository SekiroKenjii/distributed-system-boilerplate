using System.Linq.Expressions;
using Common.Core.Abstractions.Database.NoSQL;
using Common.Core.Abstractions.Entities.NoSQL;
using Common.Core.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Common.Persistence.NoSQL.MongoDB;

public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : Document
{
    private readonly IMongoCollection<TDocument> _collection;

    public MongoRepository(IMongoDbConfiguration mongoDbConfiguration)
    {
        var db = new MongoClient(mongoDbConfiguration.ConnectionString).GetDatabase(mongoDbConfiguration.DatabaseName);
        _collection = db.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
    }

    public async Task<TDocument?> FindByIdAsync(ObjectId id, CancellationToken cancellationToken = default)
    {
        return await FindOneAsync(doc => doc.Id.Equals(id), cancellationToken);
    }

    public async Task<TDocument?> FindOneAsync(Expression<Func<TDocument, bool>> predicate,
                                               CancellationToken cancellationToken = default)
    {
        return await _collection.Find(predicate).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TDocument>> FindAsync(Expression<Func<TDocument, bool>> predicate,
                                                          CancellationToken cancellationToken = default)
    {
        return await _collection.Find(predicate).ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TDocument>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await AsQueryable().ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Expression<Func<TDocument, bool>> predicate,
                                        CancellationToken cancellationToken = default)
    {
        return await _collection.Find(predicate).AnyAsync(cancellationToken);
    }

    public async Task AddAsync(TDocument entity, CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(entity, new InsertOneOptions(), cancellationToken);
    }

    public async Task UpdateAsync(TDocument entity, CancellationToken cancellationToken = default)
    {
        await _collection.FindOneAndReplaceAsync(FilterById(entity.Id), entity, cancellationToken: cancellationToken);
    }

    public async Task DeleteRangeAsync(IReadOnlyList<TDocument> entities, CancellationToken cancellationToken = default)
    {
        await _collection.DeleteManyAsync(
            Builders<TDocument>.Filter.In(
                doc => doc.Id,
                entities.Select(doc => doc.Id)
            ),
            cancellationToken
        );
    }

    public async Task DeleteAsync(Expression<Func<TDocument, bool>> predicate,
                                  CancellationToken cancellationToken = default)
    {
        await _collection.DeleteOneAsync(predicate, cancellationToken);
    }

    public async Task DeleteAsync(TDocument entity, CancellationToken cancellationToken = default)
    {
        await _collection.DeleteOneAsync(doc => doc.Id.Equals(entity.Id), cancellationToken);
    }

    public async Task DeleteByIdAsync(ObjectId id, CancellationToken cancellationToken = default)
    {
        await _collection.DeleteOneAsync(doc => doc.Id.Equals(id), cancellationToken);
    }

    public IMongoQueryable<TDocument> AsQueryable(Expression<Func<TDocument, bool>>? predicate = null)
    {
        return predicate is not null
                   ? _collection.AsQueryable().Where(predicate)
                   : _collection.AsQueryable();
    }

    private static string? GetCollectionName(Type documentType)
    {
        return ((BsonCollectionAttribute?)documentType
                                         .GetCustomAttributes(typeof(BsonCollectionAttribute), true)
                                         .FirstOrDefault())?.CollectionName;
    }

    private static FilterDefinition<TDocument> FilterById(ObjectId id)
    {
        return Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
    }
}