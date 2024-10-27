namespace Common.Core.Attributes;

/// <summary>
///     Specifies the MongoDB collection name for a class.
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class BsonCollectionAttribute(string collectionName) : Attribute
{
    /// <summary>
    ///     Gets the name of the MongoDB collection.
    /// </summary>
    public string CollectionName { get; } = collectionName;
}