using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Gamestore.MongoRepository.Entities;

[BsonIgnoreExtraElements]
public class MongoOrderDetail
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ObjectId { get; set; }

    [BsonElement("OrderID")]
    public int OrderId { get; set; }

    [BsonElement("ProductID")]
    public int ProductId { get; set; }

    [BsonElement("UnitPrice")]
    public double UnitPrice { get; set; }

    [BsonElement("Quantity")]
    public int Quantity { get; set; }

    [BsonElement("Discount")]
    public int Discount { get; set; }
}
