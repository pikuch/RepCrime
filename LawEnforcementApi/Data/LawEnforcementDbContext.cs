using LawEnforcementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LawEnforcementApi.Data;

public class LawEnforcementDbContext : DbContext
{
    public DbSet<LawEnforcementOfficer> LawEnforcementOfficers { get; set; } = null!;
    public DbSet<Rank> Ranks { get; set; } = null!;

    public LawEnforcementDbContext(DbContextOptions<LawEnforcementDbContext> options)
        : base(options)
    {
    }
}
