using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RepCrimeCommon.Enums;

namespace CrimeApi.Models;

public class CrimeEvent
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    public DateTime Date { get; set; }
    public string CrimeEventTypeId { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Place { get; set; } = null!;
    public string ReporterEmail { get; set; } = null!;
    public CrimeEventStatus Status { get; set; }
    public string? AssignedLawEnforcementId { get; set; }
}
