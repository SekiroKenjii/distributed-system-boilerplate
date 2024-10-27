namespace Common.Persistence.NoSQL.MongoDB;

public interface IMongoDbConfiguration
{
    string? ConnectionString { get; set; }

    string? DatabaseName { get; set; }
}

public class MongoDbConfiguration : IMongoDbConfiguration
{
    public string? ConnectionString { get; set; }

    public string? DatabaseName { get; set; }
}