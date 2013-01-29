using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoDbGenericDao
{
    public abstract class MongoDBEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
