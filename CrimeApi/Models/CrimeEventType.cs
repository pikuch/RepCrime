using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CrimeApi.Models;

public class CrimeEventType
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    public string EventType { get; set; } = null!;
}
