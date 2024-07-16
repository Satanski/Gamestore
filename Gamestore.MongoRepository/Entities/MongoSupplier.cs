using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Gamestore.MongoRepository.Entities;

[BsonIgnoreExtraElements]
public class MongoSupplier
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ObjectId { get; set; }

    [BsonElement("SupplierID")]
    public int SupplierID { get; set; }

    [BsonElement("CompanyName")]
    public string CompanyName { get; set; }
}
