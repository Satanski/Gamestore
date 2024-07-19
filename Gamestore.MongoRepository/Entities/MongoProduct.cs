﻿using Gamestore.MongoRepository.Helpers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Gamestore.MongoRepository.Entities;

[BsonIgnoreExtraElements]
public class MongoProduct
{
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

    [BsonElement("UnitsOnOrder")]
    public int UnitsOnOrder { get; set; }

    [BsonElement("ReorderLevel")]
    public int ReorderLevel { get; set; }

    [BsonElement("Discontinued")]
    public int Discontinued { get; set; }

    [BsonElement("QuantityPerUnit")]
    public string QuantityPerUnit { get; set; }

    [BsonIgnore]
    public List<MongoProductCategory> ProductGenres
    {
        get
        {
#pragma warning disable SA1010 // Opening square brackets should be spaced correctly
            return [new() { ProductId = GuidHelpers.IntToGuid(ProductId), CategoryId = GuidHelpers.IntToGuid(CategoryID) }];
#pragma warning restore SA1010 // Opening square brackets should be spaced correctly
        }
    }

    [BsonIgnore]
    public List<MongoProductPlatform> ProductPlatforms
    {
        get
        {
#pragma warning disable SA1010 // Opening square brackets should be spaced correctly
            return [new() { ProductId = GuidHelpers.IntToGuid(ProductId), PlatformId = Guid.Empty }];
#pragma warning restore SA1010 // Opening square brackets should be spaced correctly
        }
    }
}
