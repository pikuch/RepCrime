using System.ComponentModel.DataAnnotations;

namespace RepCrimeCommon.Dtos;

public class CrimeEventCreateDto
{
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime Date { get; set; }
    [Required]
    public string CrimeEventTypeId { get; set; } = null!;
    [Required]
    [StringLength(255, MinimumLength = 5)]
    public string Description { get; set; } = null!;
    [Required]
    [StringLength(255, MinimumLength = 5)]
    public string Place { get; set; } = null!;
    [Required]
    [StringLength(255, MinimumLength = 5)]
    [DataType(DataType.EmailAddress)]
    public string ReporterEmail { get; set; } = null!;
}
