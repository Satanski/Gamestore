using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Gamestore.MongoRepository.Entities;

public class GameAddLogEntry
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ObjectId { get; set; }

    [BsonElement("Action")]
    public string Action { get; set; }

    [BsonElement("EntityType")]
    public string EntityType { get; set; }

    [BsonElement("Value")]
    public string Value { get; set; }

    [BsonElement("PublisherId")]
    public string PublisherId { get; set; }

    [BsonElement("Genres")]
    public List<MongoGenre> Genres { get; set; }

    [BsonElement("Platforms")]
    public List<MongoPlatform> Platforms { get; set; }

    [BsonElement("Date")]
    public DateTime Date { get; set; }
}
