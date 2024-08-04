using Gamestore.MongoRepository.Helpers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Gamestore.MongoRepository.Entities;
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1010:Opening square brackets should be spaced correctly", Justification = "No way to get rid of warning")]

[BsonIgnoreExtraElements]
public class MongoProduct
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ObjectId { get; set; }

    [BsonElement("ProductID")]
    public int ProductId { get; set; }

    [BsonIgnore]
    public Guid ProductIdGuid => GuidHelpers.IntToGuid(ProductId);

    [BsonElement("ProductName")]
    public string ProductName { get; set; }

    [BsonElement("SupplierID")]
    public int SupplierID { get; set; }

    [BsonIgnore]
    public MongoPublisher Supplier => new() { Id = GuidHelpers.IntToGuid(SupplierID) };

    [BsonElement("CategoryID")]
    public int CategoryID { get; set; }

    [BsonIgnore]
    public Guid CategoryIDGuid => GuidHelpers.IntToGuid(CategoryID);

    [BsonElement("UnitPrice")]
    public double UnitPrice { get; set; }

    [BsonElement("UnitsInStock")]
    public int UnitsInStock { get; set; }

    [BsonElement("UnitsOnOrder")]
    public int UnitsOnOrder { get; set; }

    [BsonElement("ReorderLevel")]
    public int ReorderLevel { get; set; }

    [BsonElement("Discontinued")]
    public int Discontinued { get; set; }

    [BsonElement("QuantityPerUnit")]
    public string QuantityPerUnit { get; set; }

    [BsonIgnore]
    public List<MongoProductCategory> ProductGenres => [new() { ProductId = GuidHelpers.IntToGuid(ProductId), CategoryId = GuidHelpers.IntToGuid(CategoryID) }];

    [BsonIgnore]
    public List<MongoProductPlatform> ProductPlatforms => [new() { ProductId = GuidHelpers.IntToGuid(ProductId), PlatformId = Guid.Empty }];
}
