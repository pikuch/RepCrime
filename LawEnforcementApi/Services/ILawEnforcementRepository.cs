using LawEnforcementApi.Models;

namespace LawEnforcementApi.Services;

public interface ILawEnforcementRepository
{
    public Task<IEnumerable<LawEnforcementOfficer>> GetAllLawEnforcementOfficers();
    public Task<IEnumerable<Rank>> GetAllRanks();
    public Task<LawEnforcementOfficer?> GetLawEnforcementOfficerByCodename(string codename);
    public Task<Rank> Add(Rank rank);
    public Task<LawEnforcementOfficer> Add(LawEnforcementOfficer officer);
}
