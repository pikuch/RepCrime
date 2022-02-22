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

    public async Task<Rank> Add(Rank rank)
    {
        await _context.Ranks.AddAsync(rank);
        await _context.SaveChangesAsync();
        return rank;
    }

    public async Task<LawEnforcementOfficer> Add(LawEnforcementOfficer officer)
    {
        await _context.LawEnforcementOfficers.AddAsync(officer);
        await _context.SaveChangesAsync();
        return officer;
    }

    public async Task<IEnumerable<LawEnforcementOfficer>> GetAllLawEnforcementOfficers()
    {
        return await _context.LawEnforcementOfficers.ToListAsync();
    }

    public async Task<IEnumerable<Rank>> GetAllRanks()
    {
        return await _context.Ranks.ToListAsync();
    }

    public async Task<LawEnforcementOfficer?> GetLawEnforcementOfficerByCodename(string codename)
    {
        return await _context.LawEnforcementOfficers.SingleOrDefaultAsync(x => x.Codename == codename);
    }
}
