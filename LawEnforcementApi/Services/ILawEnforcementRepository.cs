using LawEnforcementApi.Models;

namespace LawEnforcementApi.Services;

public interface ILawEnforcementRepository
{
    public Task<IEnumerable<LawEnforcementOfficer>> GetAllOfficersAsync();
    public Task<IEnumerable<Rank>> GetAllRanksAsync();
    public Task<LawEnforcementOfficer?> GetOfficerByCodenameAsync(string codename);
    public Task<Rank> AddNewRankAsync(Rank rank);
    public Task<LawEnforcementOfficer> AddNewOfficerAsync(LawEnforcementOfficer officer);
}
