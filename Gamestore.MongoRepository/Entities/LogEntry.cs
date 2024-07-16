using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Gamestore.MongoRepository.Entities;

public class LogEntry
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

    [BsonElement("Date")]
    public DateTime Date { get; set; }
}
