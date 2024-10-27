using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Common.Core.Abstractions.Entities.NoSQL;

/// <summary>
///     Represents a document in a NoSQL database.
/// </summary>
public interface IDocument : IEntity<ObjectId>
{
    /// <summary>
    ///     Gets the creation date and time.
    /// </summary>
    DateTimeOffset CreatedAt { get; }

    /// <summary>
    ///     Gets or sets the modification date and time.
    /// </summary>
    DateTimeOffset? ModifiedAt { get; set; }
}

/// <summary>
///     Represents a document in a NoSQL database with an ObjectId as the primary key.
/// </summary>
public abstract class Document : IDocument
{
    /// <summary>
    ///     Gets or sets the document identifier.
    /// </summary>
    public Guid DocumentId { get; set; }

    /// <inheritdoc />
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public ObjectId Id { get; set; }

    /// <inheritdoc />
    public DateTimeOffset CreatedAt => Id.CreationTime;

    /// <inheritdoc />
    public DateTimeOffset? ModifiedAt { get; set; }
}