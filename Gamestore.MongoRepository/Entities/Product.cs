using Gamestore.MongoRepository.Helpers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Gamestore.MongoRepository.Entities;

[BsonIgnoreExtraElements]
public class Product
{
    private const string PhysicalProductGuid = "11111111-1111-1111-1111-111111111111";

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ObjectId { get; set; }

    [BsonElement("ProductID")]
    public int ProductId { get; set; }

    [BsonIgnore]
    public Guid ProductIdGuid
    {
        get
        {
            return GuidHelpers.IntToGuid(ProductId);
        }
    }

    [BsonElement("ProductName")]
    public string ProductName { get; set; }

    [BsonElement("SupplierID")]
    public int SupplierID { get; set; }

    [BsonIgnore]
    public MongoPublisher Supplier
    {
        get
        {
            return new MongoPublisher() { Id = GuidHelpers.IntToGuid(SupplierID) };
        }
    }

    [BsonElement("CategoryID")]
    public int CategoryID { get; set; }

    [BsonIgnore]
    public Guid CategoryIDGuid
    {
        get
        {
            return GuidHelpers.IntToGuid(CategoryID);
        }
    }

    [BsonElement("UnitPrice")]
    public double UnitPrice { get; set; }

    [BsonElement("UnitsInStock")]
    public int UnitsInStock { get; set; }

    [BsonElement("Discontinued")]
    public int Discontinued { get; set; }

    [BsonElement("QuantityPerUnit")]
    public string QuantityPerUnit { get; set; }

    [BsonIgnore]
    public List<MongoGameGenre> GameGenres
    {
        get
        {
#pragma warning disable SA1010 // Opening square brackets should be spaced correctly
            return [new() { GameId = GuidHelpers.IntToGuid(ProductId), GenreId = GuidHelpers.IntToGuid(CategoryID) }];
#pragma warning restore SA1010 // Opening square brackets should be spaced correctly
        }
    }

    [BsonIgnore]
    public List<MongoGamePlatform> GamePlatforms
    {
        get
        {
#pragma warning disable SA1010 // Opening square brackets should be spaced correctly
            return [new() { GameId = GuidHelpers.IntToGuid(ProductId), PlatformId = new Guid(PhysicalProductGuid) }];
#pragma warning restore SA1010 // Opening square brackets should be spaced correctly
        }
    }
}
