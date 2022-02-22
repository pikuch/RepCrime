using LawEnforcementApi.Data;
using LawEnforcementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LawEnforcementApi.Services;

public class LawEnforcementRepository : ILawEnforcementRepository
{
    private readonly LawEnforcementDbContext _context;

    public LawEnforcementRepository(LawEnforcementDbContext context)
    {
        _context = context;
    }

    public async Task<Rank> AddNewRankAsync(Rank rank)
    {
        await _context.Ranks.AddAsync(rank);
        await _context.SaveChangesAsync();
        return rank;
    }

    public async Task<LawEnforcementOfficer> AddNewOfficerAsync(LawEnforcementOfficer officer)
    {
        await _context.LawEnforcementOfficers.AddAsync(officer);
        await _context.SaveChangesAsync();
        return officer;
    }

    public async Task<IEnumerable<LawEnforcementOfficer>> GetAllOfficersAsync()
    {
        return await _context.LawEnforcementOfficers.Include(x => x.Rank).ToListAsync();
    }

    public async Task<IEnumerable<Rank>> GetAllRanksAsync()
    {
        return await _context.Ranks.ToListAsync();
    }

    public async Task<LawEnforcementOfficer?> GetOfficerByCodenameAsync(string codename)
    {
        return await _context.LawEnforcementOfficers.Include(x => x.Rank).SingleOrDefaultAsync(x => x.Codename == codename);
    }
}
