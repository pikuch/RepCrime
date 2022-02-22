namespace LawEnforcementApi.Models;

public class LawEnforcementOfficer
{
    public int Id { get; set; }
    public string Codename { get; set; } = null!;
    public int RankId { get; set; }
    public Rank Rank { get; set; } = null!;
}
