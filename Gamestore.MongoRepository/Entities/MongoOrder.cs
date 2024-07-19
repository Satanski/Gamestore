using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Gamestore.MongoRepository.Entities;

[BsonIgnoreExtraElements]
public class MongoOrder
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ObjectId { get; set; }

    [BsonElement("OrderID")]
    public int OrderId { get; set; }

    [BsonElement("CustomerID")]
    public string CustomerId { get; set; }

    [BsonElement("EmployeeID")]
    public int EmployeeId { get; set; }

    [BsonElement("OrderDate")]
    public string OrderDate { get; set; }

    [BsonElement("RequiredDate")]
    public string RequireDate { get; set; }

    [BsonElement("ShippedDate")]
    public string ShippedDate { get; set; }

    [BsonElement("ShipVia")]
    public int ShipVia { get; set; }

    [BsonElement("Freight")]
    public double Freight { get; set; }

    [BsonElement("ShipName")]
    public string ShipName { get; set; }

    [BsonElement("ShipAddress")]
    public dynamic ShipAddress { get; set; }

    [BsonElement("ShipCity")]
    public dynamic ShipCity { get; set; }

    [BsonElement("ShipRegion")]
    public dynamic? ShipRegion { get; set; }

    [BsonElement("ShipPostalCode")]
    public dynamic ShipPostalCode { get; set; }

    [BsonElement("ShipCountry")]
    public dynamic ShipCountry { get; set; }
}
