using RepCrimeCommon.Dtos;

namespace RepCrimeGateway.Services;

public interface ILawEnforcementService
{
    public Task<IEnumerable<LawEnforcementOfficerReadDto>?> GetAllOfficersAsync();
    public Task<LawEnforcementOfficerReadDto?> GetOfficerByCodenameAsync(string codename);
    public Task<LawEnforcementOfficerReadDto?> AddNewOfficerAsync(LawEnforcementOfficerCreateDto officerCreateDto);
    public Task<IEnumerable<RankReadDto>?> GetAllRanksAsync();
    public Task<bool> AddNewRankAsync(RankCreateDto rankCreateDto);
}
