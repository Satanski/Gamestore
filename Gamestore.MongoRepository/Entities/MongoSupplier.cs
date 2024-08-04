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

    [BsonElement("ContactName")]
    public string ContactName { get; set; }

    [BsonElement("ContactTitle")]
    public string ContactTitle { get; set; }

    [BsonElement("Address")]
    public dynamic Address { get; set; }

    [BsonElement("City")]
    public dynamic City { get; set; }

    [BsonElement("Region")]
    public string Region { get; set; }

    [BsonElement("Country")]
    public dynamic Country { get; set; }

    [BsonElement("Phone")]
    public dynamic Phone { get; set; }

    [BsonElement("Fax")]
    public dynamic Fax { get; set; }

    [BsonElement("HomePage")]
    public string HomePage { get; set; }
}
