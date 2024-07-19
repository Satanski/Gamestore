using Gamestore.MongoRepository.LoggingModels;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Gamestore.MongoRepository.Entities;

public class GameUpdateLogEntry
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ObjectId { get; set; }

    [BsonElement("Action")]
    public string Action { get; set; }

    [BsonElement("EntityType")]
    public string EntityType { get; set; }

    [BsonElement("OldValue")]
    public string OldValue { get; set; }

    [BsonElement("NewValue")]
    public string NewValue { get; set; }

    [BsonElement("OldPublisherId")]
    public string OldPublisherId { get; set; }

    [BsonElement("NewPublisherId")]
    public string NewPublisherId { get; set; }

    [BsonElement("OldGenres")]
    public List<MongoGenre> OldGenres { get; set; }

    [BsonElement("OldPlatforms")]
    public List<MongoPlatform> OldPlatforms { get; set; }

    [BsonElement("NewGenres")]
    public List<MongoGenre> NewGenres { get; set; }

    [BsonElement("NewPlatforms")]
    public List<MongoPlatform> NewPlatforms { get; set; }

    [BsonElement("Date")]
    public DateTime Date { get; set; }
}
