using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Primitives;

public abstract class MongoEntity
{
    public string Id { get; private set; }

    protected MongoEntity(Guid id)
        => Id = id.ToString();
}