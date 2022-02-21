using System.ComponentModel.DataAnnotations;

namespace RepCrimeCommon.Dtos;

public class CrimeEventTypeCreateDto
{
    [Required]
    [StringLength(64, MinimumLength = 3)]
    public string EventType { get; set; } = null!;
}
