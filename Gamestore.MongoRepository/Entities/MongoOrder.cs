using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Gamestore.MongoRepository.Entities;

public class MongoOrder
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ObjectId { get; set; }

    [BsonElement("OrderId")]
    public int OrderId { get; set; }

    [BsonElement("CustomerId")]
    public int CustomerId { get; set; }

    [BsonElement("EmployeeId")]
    public int EmployeeId { get; set; }

    [BsonElement("OrderDate")]
    public DateTime OrderDate { get; set; }

    [BsonElement("RequirDate")]
    public DateTime RequireDate { get; set; }

    [BsonElement("ShippedDate")]
    public DateTime ShippedDate { get; set; }

    [BsonElement("ShipVia")]
    public int ShipVia { get; set; }

    [BsonElement("Freight")]
    public double Freight { get; set; }

    [BsonElement("ShipName")]
    public string ShipName { get; set; }

    [BsonElement("ShipAddress")]
    public string ShipAddress { get; set; }

    [BsonElement("ShipCity")]
    public string ShipCity { get; set; }

    [BsonElement("ShipRegion")]
    public string? ShipRegion { get; set; }

    [BsonElement("ShipPostalCode")]
    public int ShipPostalCode { get; set; }

    [BsonElement("ShipCountry")]
    public string ShipCountry { get; set; }
}
