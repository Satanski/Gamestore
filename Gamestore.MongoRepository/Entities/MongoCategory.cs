using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Gamestore.MongoRepository.Entities;

[BsonIgnoreExtraElements]
public class MongoCategory
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ObjectId { get; set; }

    [BsonElement("CategoryID")]
    public int CategoryId { get; set; }

    [BsonElement("CategoryName")]
    public string CategoryName { get; set; }
}
