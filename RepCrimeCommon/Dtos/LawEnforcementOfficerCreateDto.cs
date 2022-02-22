using System.ComponentModel.DataAnnotations;

namespace RepCrimeCommon.Dtos;

public class LawEnforcementOfficerCreateDto
{
    [Required]
    [StringLength(4, MinimumLength = 4)]
    public string Codename { get; set; } = null!;
    public int RankId { get; set; }
}
