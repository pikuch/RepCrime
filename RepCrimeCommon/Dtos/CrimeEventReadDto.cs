using RepCrimeCommon.Enums;

namespace RepCrimeCommon.Dtos;

public class CrimeEventReadDto
{
    public string Id { get; set; } = null!;
    public DateTime Date { get; set; }
    public string CrimeEventTypeId { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Place { get; set; } = null!;
    public string ReporterEmail { get; set; } = null!;
    public CrimeEventStatus Status { get; set; }
    public string? AssignedLawEnforcementId { get; set; }
}
