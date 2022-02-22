using System.ComponentModel.DataAnnotations;

namespace RepCrimeCommon.Dtos;

public class RankCreateDto
{
    [Required]
    [StringLength(32, MinimumLength = 3)]
    public string Name { get; set; } = null!;
}
